using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing;
using Mossarium.Alpha.UI.Windowing.Structures;
using System.Diagnostics;
using WindowsOS;
using WindowsOS.Utils;

namespace Mossarium.Alpha.UI.Windows;

public unsafe class GraphicsSystemWindow : SystemWindow, IDisposable
{
#if !UI_Unlimited_FPS
    const int DefaultFramesPerSecond = 30;
    const int SecondsBeforeRest = 20;
    const int FramesBeforeRest = SecondsBeforeRest * DefaultFramesPerSecond;
    static readonly FrameRateInfo DefaultFrameRateInfo = new FrameRateInfo(DefaultFramesPerSecond);
    static readonly FrameRateInfo InRestFrameRateInfo = new FrameRateInfo(5);
#endif

    public GraphicsSystemWindow(string title, LocationI4 location, SizeI4 size, WindowInitialAttributes attributes = WindowInitialAttributes.None)
        : base(title, location, size)
    {
        ApplyWindowAttributes(attributes);
    }

#if !UI_Unlimited_FPS
    FrameRateInfo frameRateInfo = DefaultFrameRateInfo;
    int framesBeforeRest;
#endif
    WindowGLContext glContext;

    void ApplyWindowAttributes(WindowInitialAttributes attributes)
    {
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

    protected virtual void OnWindowInitialized() { }

    protected override void OnMessageLoopStarted()
    {
        var thread = new Thread(StartRenderLoop)
        {
            Name = $"UI Render Loop Thread 0x{Handle:X}"
        };

        thread.Start();
    }

    protected virtual void OnRender() { }

#if !UI_Unlimited_FPS
    protected override unsafe bool OnMessage(nint hWnd, WindowMessage message, ulong wParam, ulong lParam)
    {
        if (framesBeforeRest <= 0)
            frameRateInfo = DefaultFrameRateInfo;

        framesBeforeRest = FramesBeforeRest;
        return base.OnMessage(hWnd, message, wParam, lParam);
    }
#endif

    void StartRenderLoop()
    {
#if !UI_Unlimited_FPS
        var frameRateWatcher = new Stopwatch();
        using var frameTimer = new HighAccuracyWaitableTimer();
#endif
        glContext = new WindowGLContext(HandleToDeviceContext);
        OnWindowInitialized();

        while (isRunning)
        {
#if !UI_Unlimited_FPS
            frameRateWatcher.Restart();

            framesBeforeRest--;
            if (framesBeforeRest == 0)
                frameRateInfo = InRestFrameRateInfo;
#endif

            OnRender();

#if !UI_Unlimited_FPS
            var spentOnFrame = frameRateWatcher.Elapsed.TotalMilliseconds;

            var timeToSpend = frameRateInfo.FrameDelay - spentOnFrame;
            if (timeToSpend > 0)
            {
                var nanoseconds = (long)(timeToSpend * 10000);
                frameTimer.Wait(nanoseconds);
            }
#endif
        }

#if !UI_Unlimited_FPS
        frameRateWatcher.Stop();
#endif
    }

    public new void Dispose()
    {
        glContext.Dispose();
        base.Dispose();
    }

#if !UI_Unlimited_FPS
    struct FrameRateInfo
    {
        public FrameRateInfo(int framesPerSecond)
        {
            FramesPerSecond = framesPerSecond;
            FrameDelay = 1000d / FramesPerSecond;
            FloorFrameDelay = (int)FrameDelay;
        }

        public int FramesPerSecond { get; init; }
        public double FrameDelay { get; init; }
        public int FloorFrameDelay { get; init; }
    }
#endif
}