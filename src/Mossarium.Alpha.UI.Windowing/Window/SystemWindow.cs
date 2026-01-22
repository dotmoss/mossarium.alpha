using Mossarium.Alpha.UI.Windowing.Structures;
using System.Runtime.CompilerServices;
using WindowsOS;

namespace Mossarium.Alpha.UI.Windowing;

public unsafe abstract partial class SystemWindow : IDisposable
{
    public SystemWindow(string title, LocationI4 location, SizeI4 size, WindowInitialAttributes attributes)
    {
        NativeWindow = NativeWindow.Create(title, location, size);

        var styles = WindowStyles.Overlapped;
        if ((attributes & WindowInitialAttributes.HasCaption) > 0)
        {
            styles = WindowStyles.Caption | WindowStyles.SizeFrame;
            if ((attributes & WindowInitialAttributes.HasMinimizeButton) > 0)
                styles |= WindowStyles.MinimizeBox;
            if ((attributes & WindowInitialAttributes.HasMaximizeButton) > 0)
                styles |= WindowStyles.MaximizeBox;
            if ((attributes & WindowInitialAttributes.HasCloseButton) > 0)
                styles |= WindowStyles.SystemMenu;
        }

        if ((attributes & WindowInitialAttributes.Maximaze) > 0)
            styles |= WindowStyles.Maximize;

        Style = styles;
    }

    NativeWindow nativeWindow;
    RectangleI4 cachedRectangle;

    public NativeWindow NativeWindow
    {
        get => nativeWindow;
        set
        {
            nativeWindow = value;

            var oldCachedRectangle = cachedRectangle;
            cachedRectangle = nativeWindow.Rectangle;

            if (oldCachedRectangle.X != 0 || oldCachedRectangle.Y != 0 || oldCachedRectangle.Width != 0 || oldCachedRectangle.Height != 0)
            {
                OnLocationChanged(X, Y);
                OnSizeChanged(Width, Height);
            }
        }
    }

    public string Title { get => nativeWindow.Title; set => nativeWindow.Title = value; }

    public RectangleI4 Rectangle
    { 
        get => cachedRectangle; 
        set => nativeWindow.Rectangle = value;
    }

    public LocationI4 Location 
    {
        get => cachedRectangle.Location;
        set => nativeWindow.Location = cachedRectangle.Location = value;
    }

    public SizeI4 Size
    {
        get => cachedRectangle.Size;
        set => nativeWindow.Size = cachedRectangle.Size = value;
    }

    public int X
    {
        get => cachedRectangle.X;
        set => nativeWindow.Rectangle = cachedRectangle = cachedRectangle with { X = value };
    }

    public int Y
    {
        get => cachedRectangle.Y;
        set => nativeWindow.Rectangle = cachedRectangle = cachedRectangle with { Y = value };
    }

    public int Width 
    {
        get => cachedRectangle.Width;
        set => nativeWindow.Rectangle = cachedRectangle = cachedRectangle with { Width = value };
    }

    public int Height
    {
        get => cachedRectangle.Height;
        set => nativeWindow.Rectangle = cachedRectangle = cachedRectangle with { Height = value };
    }

    public WindowStyles Style { get => nativeWindow.Style; set => nativeWindow.Style = value; }
        
    public WindowExStyles ExStyle { get => nativeWindow.ExStyle; set => nativeWindow.ExStyle = value; }

    public bool Visible
    {
        get => (Style & WindowStyles.Visible) != 0;
        set => Style = value ? Style | WindowStyles.Visible : Style & ~WindowStyles.Visible;
    }

    public bool Layered
    {
        set
        {
            if (value)
            {
                const int LWA_COLORKEY = 1;

                ExStyle |= WindowExStyles.Layered;
                User32.SetLayeredWindowAttributes(Handle, TransparentColor.Win32Value, default, LWA_COLORKEY);
            }
            else
            {
                ExStyle &= ~WindowExStyles.Layered;
                User32.SetLayeredWindowAttributes(Handle, default, default, default);
            }
        }
    }

    public nint Handle => nativeWindow.Handle;

    public nint DeviceContextHandle => nativeWindow.HDC;

    protected virtual bool OnMessage(nint hWnd, WindowMessage message, ulong wParam, ulong lParam)
    {
        //Console.WriteLine($"{message}: (0x{wParam:X}, 0x{lParam})");

        switch (message)
        {
            case WindowMessage.Size:
                {
                    const ulong SIZE_MAXHIDE = 4;
                    const ulong SIZE_MAXSHOW = 4;

                    if (wParam is not SIZE_MAXHIDE and not SIZE_MAXSHOW)
                    {
                        var size = Message.DecodeSize(lParam);
                        Size = new SizeI4(size.Width, size.Height);
                        OnSizeChanged(size.Width, size.Height);
                    }

                    break;
                }
            case WindowMessage.Move:
                {
                    var location = Message.DecodeLocation(lParam);

                    Location = new LocationI4(location.X, location.Y);
                    OnLocationChanged(location.X, location.Y);
                    break;
                }
            case WindowMessage.KeyDown:
                {
                    var downKey = Message.DecodeDownKey(wParam, lParam);
                    var key = downKey.Key;

                    if (downKey.IsRepeat)
                    {
                        OnKeyPressed(key);
                    }
                    else
                    {
                        PressedKeyCollection.Insance.NotifyPress(key);
                        OnKeyDown(key);
                    }

                    break;
                }
            case WindowMessage.KeyUp:
                {
                    var downKey = Message.DecodeDownKey(wParam, lParam);
                    var key = downKey.Key;

                    PressedKeyCollection.Insance.NotifyUnpress(key);
                    OnKeyUp(key);
                    break;
                }
            case WindowMessage.Char:
                {
                    OnInput((char)wParam);
                    break;
                }
            case WindowMessage.LButtonDown:
                {
                    OnMouseLeftDown();
                    break;
                }
            case WindowMessage.RButtonDown:
                {
                    OnMouseRightDown();
                    break;
                }
            case WindowMessage.LButtonUp:
                {
                    OnMouseLeftUp();
                    OnMouseLeftClick();
                    break;
                }
            case WindowMessage.RButtonUp:
                {
                    OnMouseRightUp();
                    OnMouseRightClick();
                    break;
                }
            case WindowMessage.LButtonDoubleClick:
                {
                    OnMouseLeftDoubleClick();
                    break;
                }
            case WindowMessage.RButtonDoubleClick:
                {
                    OnMouseRightDoubleClick();
                    break;
                }
            case WindowMessage.Activate:
                {
                    var isActive = wParam == 1;
                    
                    if (isActive)
                    {
                        OnActive();
                    }
                    else
                    {
                        var pressedKeys = stackalloc Keys[264];
                        var count = PressedKeyCollection.Insance.WriteOutPressedKeys(pressedKeys);
                        for (var index = 0; index < count; index++)
                        {
                            var key = pressedKeys[index];
                            OnKeyUp(key);
                        }

                        PressedKeyCollection.Insance.Clear();

                        OnDeactive();
                    }

                    break;
                }
            case WindowMessage.Close:
            case WindowMessage.Quit:
            case WindowMessage.Command when wParam == 0x02:
                {
                    OnClose();
                    Dispose();
                    break;
                }
        }

        return true;
    }

    protected virtual void OnSizeChanged(int width, int height) { }
    protected virtual void OnLocationChanged(int x, int y) { }
    protected virtual void OnKeyPressed(Keys key) { }
    protected virtual void OnKeyDown(Keys key) { }
    protected virtual void OnKeyUp(Keys key) { }
    protected virtual void OnInput(char symbol) { }
    protected virtual void OnMouseLeftDown() { }
    protected virtual void OnMouseRightDown() { }
    protected virtual void OnMouseLeftUp() { }
    protected virtual void OnMouseRightUp() { }
    protected virtual void OnMouseLeftClick() { }
    protected virtual void OnMouseRightClick() { }
    protected virtual void OnMouseLeftDoubleClick() { }
    protected virtual void OnMouseRightDoubleClick() { }
    protected virtual void OnActive() { }
    protected virtual void OnDeactive() { }
    protected virtual void OnClose() { }

    public void Dispose()
    {
        nativeWindow.Dispose();
    }

    public static Rgb TransparentColor = (56, 30, 12);

    public static class Dispatcher
    {
        public static bool OnMessage(SystemWindow self, nint hWnd, WindowMessage message, ulong wParam, ulong lParam) => self.OnMessage(hWnd, message, wParam, lParam);
    }
}