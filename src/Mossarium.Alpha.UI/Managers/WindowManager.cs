using Mossarium.Alpha.UI.Windowing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WindowsOS;
using WindowsOS.Utils;

namespace Mossarium.Alpha.UI.Managers;

public static unsafe class WindowManager
{
    static List<Window> windows = new List<Window>();
    static StaticWindowArray staticWindows;

    public static void CloseAll()
    {
        while (windows.Count != 0)
        {
            var window = windows[0];
            window.Dispose();
        }
    }

    public static void TransferUIThreadControl()
    {
        StartLoop();
    }

    static void StartLoop()
    {
        Thread.CurrentThread.Name = "UI Thread";

        using var frameTimer = new HighAccuracyWaitableTimer();
        frameTimer.ResetTimer(0);

        var handles = stackalloc nint[2];

        const int TimerIndex = 0;
        handles[0] = frameTimer.Handle;

        const int UIJobsIndex = 1;
        handles[1] = ThreadManager.UIJobs.Handle;

        Message message;
        while (true)
        {
            var result = User32.MsgWaitForMultipleObjectsEx(
                2,
                handles,
                uint.MaxValue,
                QueueStatus.AllInput,
                MessageWMO.InputAvailable | MessageWMO.Alertable
            );

            if (result == TimerIndex)
            {
                frameTimer.ResetTimer(160000);

                for (var i = 0; i < windows.Count; i++)
                {
                    var window = windows[i];

                    var privateData = window.PrivateWindowManagerData;
                    if (privateData.MessageCounter == privateData.LastFrameMessageCounter)
                    {
                        if (privateData.FramesWithNoMessage > 1000)
                            continue;

                        privateData.FramesWithNoMessage++;
                    }
                    else
                    {
                        privateData.LastFrameMessageCounter = privateData.MessageCounter;
                        privateData.FramesWithNoMessage = 0;
                    }
                    window.PrivateWindowManagerData = privateData;

                    Window.Dispatcher.OnRender(window);
                }
            }
            else if (result == UIJobsIndex)
            {
                while (ThreadManager.UIJobs.TryTake(out var job))
                {
                    job!.Invoke();
                }
            }
            else
            {
                const int PM_REMOVE = 1;

                while (User32.PeekMessage(&message, IntPtr.Zero, 0, 0, PM_REMOVE) > 0)
                {
                    User32.TranslateMessage(&message);
                    User32.DispatchMessage(&message);
                }
            }
        }
    }

    static void NotifyWindowAdding(Window window)
    {
        windows.Add(window);
        OpenGlManager.NotifyNewWindow(window);
    }

    public static void NotifyWindowDeleting(Window window)
    {
        windows.Remove(window);
        staticWindows.Remove(window);
    }

    public static void InitializeWindow(Window window)
    {
        ThreadManager.EnsureUIThread();

        var windowPointer = staticWindows.Add(window);
        NotifyWindowAdding(window);
        User32.SetWindowLongPtr(window.Handle, WindowLongField.UserData, windowPointer);
        User32.SetWindowWndProcFunction(window.Handle, (nint)(delegate* unmanaged<nint, WindowMessage, ulong, ulong, long>)&WndProc);
        Window.Dispatcher.OnRendererInitialized(window);
    }

    [UnmanagedCallersOnly]
    static long WndProc(nint hWnd, WindowMessage message, ulong wParam, ulong lParam)
    {
        var windowAddress = User32.GetWindowLongPtr(hWnd, WindowLongField.UserData);
        var window = Unsafe.AsRef<Window>((void*)windowAddress);

        if (window is null)
            goto DEFAULT_PROC;

        if (message is not WindowMessage.GetIcon)
        {
            var privateData = window.PrivateWindowManagerData;
            privateData.MessageCounter++;
            window.PrivateWindowManagerData = privateData;
        }

        if (SystemWindow.Dispatcher.OnMessage(window, hWnd, message, wParam, lParam))
            goto DEFAULT_PROC;

        return 0;

        DEFAULT_PROC:
        return User32.CallDefaultWindowProccess(hWnd, message, wParam, lParam);
    }

    public struct PrivateWindowManagerData
    {
        public int MessageCounter;
        public int LastFrameMessageCounter;
        public int FramesWithNoMessage;
    }

    struct StaticWindowArray
    {
        Window window1, window2, window3, window4, window5, window6, window7, window8, window9, window10, window11, window12, window13, window14, window15, window16;

        public nint Add(Window window)
        {
            ref Window array = ref Unsafe.As<StaticWindowArray, Window>(ref this);
            for (var i = 0U; i < 16; i++)
            {
                if (Unsafe.Add(ref array, i) == null)
                {
                    ref Window windowAtIndex = ref Unsafe.Add(ref array, i);
                    windowAtIndex = window;
                    return (nint)Unsafe.AsPointer(ref windowAtIndex);
                }
            }

            throw null!;
        }

        public void Remove(Window window)
        {
            var array = this;
            var pointer = (Window*)&array;
            for (var i = 0U; i < 16; i++)
            {
                if (pointer[i] == window)
                {
                    pointer[i] = null;
                    return;
                }
            }

            throw null!;
        }
    }
}