using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using WindowsOS;

namespace Mossarium.Alpha.UI.Windowing;

public unsafe partial class SystemWindow : IDisposable
{
    public SystemWindow(string title, (int X, int Y) location, (int Width, int Height) size)
    {
        internalWindow = SystemWindowInternal.Create(title, location, size);
        UpdateProperties();
    }

    protected bool isRunning;
    readonly SystemWindowInternal internalWindow;
    (int X, int Y, int Width, int Height) cachedRectangle;

    public string Title { get => internalWindow.Title; set => internalWindow.Title = value; }

    public (int X, int Y, int Width, int Height) Rectangle
    { 
        get => cachedRectangle; 
        set => internalWindow.Rectangle = value;
    }

    public (int X, int Y) Location 
    { 
        get
        {
            var rectangle = cachedRectangle;
            return (rectangle.X, rectangle.Y);
        }
        set
        {
            var rectangle = cachedRectangle;
            cachedRectangle = (value.X, value.Y, rectangle.Width, rectangle.Height);
            internalWindow.Location = value;
        }
    }

    public (int Width, int Height) Size
    { 
        get
        {
            var rectangle = cachedRectangle;
            return (rectangle.Width, rectangle.Height);
        }
        set
        {
            var rectangle = cachedRectangle;
            cachedRectangle = (rectangle.X, rectangle.Y, value.Width, value.Height);
            internalWindow.Size = value;
        }
    }

    public int Width 
    {
        get => cachedRectangle.Width;
        set
        {
            var rectangle = cachedRectangle;
            internalWindow.Rectangle = cachedRectangle = (rectangle.X, rectangle.Y, value, rectangle.Height);
        }
    }

    public int Height
    {
        get => cachedRectangle.Height;
        set
        {
            var rectangle = cachedRectangle;
            internalWindow.Rectangle = cachedRectangle = (rectangle.X, rectangle.Y, rectangle.Width, value);
        }
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public WindowStyles Style { get => internalWindow.Style; set => internalWindow.Style = value; }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public WindowExStyles ExStyle { get => internalWindow.ExStyle; set => internalWindow.ExStyle = value; }

    public bool Visible
    {
        get => (Style & WindowStyles.Visible) != 0;
        set => Style = value ? Style | WindowStyles.Visible : Style & ~WindowStyles.Visible;
    }

    public nint Handle => internalWindow.Handle;

    public nint HandleToDeviceContext => internalWindow.HDC;
    
    public bool IsActive { get; private set; }
    
    void UpdateProperties()
    {
        cachedRectangle = internalWindow.Rectangle;
    }

    public void TransferThreadControl()
    {
        StartMessageLoop();
    }

    protected virtual bool OnMessage(nint hWnd, WindowMessage message, ulong wParam, ulong lParam)
    {
        //Debug.WriteLine($"{message}: (0x{wParam:X}, 0x{lParam})");

        switch (message)
        {
            case WindowMessage.Close:
            case WindowMessage.Quit:
            case WindowMessage.Command when wParam == 0x02:
                {                    
                    return OnClose();
                }
            case WindowMessage.Char:
                {
                    return OnInput((char)wParam);
                }
            case WindowMessage.KeyDown:
                {
                    var downKey = Message.DecodeDownKey(wParam, lParam);

                    if (downKey.IsRepeat)
                        return OnKeyPress(downKey.Key);
                    else return OnKeyDownInternal(downKey.Key);
                }
            case WindowMessage.KeyUp:
                {
                    var downKey = Message.DecodeDownKey(wParam, lParam);

                    return OnKeyUpInternal(downKey.Key);
                }
            case WindowMessage.Activate:
                {
                    var isActive = wParam == 1;
                    IsActive = isActive;
                    OnActiveChanged(isActive);

                    break;
                }
        }

        return true;
    }

    protected virtual void OnActiveChanged(bool isActive)
    {
        if (!isActive)
        {
            var pressedKeys = stackalloc Keys[264];
            var count = PressedKeyCollection.Insance.WriteOutPressedKeys(pressedKeys);
            for (var index = 0; index < count; index++)
            {
                var key = pressedKeys[index];
                OnKeyUp(key);
            }

            PressedKeyCollection.Insance.Clear();
        }
    }

    protected virtual bool OnClose() 
    {
        Dispose();
        return true;
    }

    protected virtual bool OnKeyPress(Keys key)
    {
        return true;
    }

    protected bool OnKeyDownInternal(Keys key)
    {
        PressedKeyCollection.Insance.NotifyPress(key);
        return OnKeyDown(key);
    }

    protected virtual bool OnKeyDown(Keys key)
    {
        return true;
    }

    protected bool OnKeyUpInternal(Keys key)
    {
        PressedKeyCollection.Insance.NotifyUnpress(key);
        return OnKeyUp(key);
    }

    protected virtual bool OnKeyUp(Keys key)
    {
        return true;
    }

    protected virtual bool OnInput(char @char)
    {
        return true;
    }

    protected virtual void OnMessageLoopStarted() { }

    void StartMessageLoop()
    {
        if (isRunning)
            throw new Exception($"{nameof(SystemWindow)}->{nameof(StartMessageLoop)}: UI Thread is already running");
        isRunning = true;

        Thread.CurrentThread.Name = $"UI Message Loop Thread 0x{Handle:X}";
        ThreadLocalInstance = this;

        OnMessageLoopStarted();

        User32.SetWindowWndProcFunction(Handle, (nint)(delegate* unmanaged<nint, WindowMessage, ulong, ulong, long>)&WndProc);
        
        Message message;
        while (isRunning)
        {
            internalWindow.GetMessage(&message);
            internalWindow.TranslateMessage(&message);
            internalWindow.DispatchMessage(&message);
        }

        ThreadLocalInstance = null;
    }

    [UnmanagedCallersOnly]
    static long WndProc(nint hWnd, WindowMessage message, ulong wParam, ulong lParam)
    {
        var instance = ThreadLocalInstance;
        Debug.Assert(instance is not null);

        if (instance.OnMessage(hWnd, message, wParam, lParam))
            return User32.CallDefaultWindowProccess(instance.Handle, message, wParam, lParam);
        
        return 0;
    }

    public void Dispose()
    {
        isRunning = false;
    }

    // Otherwise use dynamically compiled thunk. But is not it overengineering? :)
    [ThreadStatic, AllowNull]
    static SystemWindow ThreadLocalInstance;
}