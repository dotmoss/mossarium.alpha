using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing;
using OpenGL;
using System.Diagnostics;
using WindowsOS;
using WindowsOS.Utils;

namespace Mossarium.Alpha.UI;

public unsafe class Window : SystemWindow
{
    const int DefaultFramesPerSecond = 30;
    const int SecondsBeforeRest = 20;
    const int FramesBeforeRest = SecondsBeforeRest * DefaultFramesPerSecond;
    static readonly FrameRate DefaultFrameRate = new FrameRate(DefaultFramesPerSecond);
    static readonly FrameRate InRestFrameRate = new FrameRate(5);

    public Window(string title, (int X, int Y) location, (int Width, int Height) size, WindowAttributes attributes = WindowAttributes.None)
        : base(title, location, size)
    {
        SetWindowAttributes(attributes);

        glContext = new WindowGLContext(HandleToDeviceContext);
        frameRateWatcher = new Stopwatch();
        frameTimer = new HighAccuracyWaitableTimer();
    }

    WindowGLContext glContext;
    Stopwatch frameRateWatcher;
    HighAccuracyWaitableTimer frameTimer;
    FrameRate frameRate = DefaultFrameRate;
    int framesBeforeRest;

    void SetWindowAttributes(WindowAttributes attributes)
    {
        var styles = WindowStyles.Overlapped;
        if ((attributes & WindowAttributes.HasCaption) > 0)
        {
            styles = WindowStyles.Caption | WindowStyles.SizeFrame;
            if ((attributes & WindowAttributes.HasMinimizeButton) > 0)
                styles |= WindowStyles.MinimizeBox;
            if ((attributes & WindowAttributes.HasMaximizeButton) > 0)
                styles |= WindowStyles.MaximizeBox;
            if ((attributes & WindowAttributes.HasCloseButton) > 0)
                styles |= WindowStyles.SystemMenu;
        }

        if ((attributes & WindowAttributes.Maximaze) > 0)
            styles |= WindowStyles.Maximize;

        Style = styles;
    }

    protected void InitializeWindow()
    {
        Visible = true;
    }

    protected virtual void OnInitialized() { }

    protected override void OnRender()
    {
        frameRateWatcher.Restart();

        framesBeforeRest--;
        if (framesBeforeRest == 0)
            frameRate = InRestFrameRate;

        GL.Clear(0x4000 | 0x100);

        GL.ClearColor(Random.Shared.NextSingle(), 0, 0, 1);

        GL.SwapBuffers(HandleToDeviceContext);  
        base.OnRender();

        var spentOnFrame = frameRateWatcher.Elapsed.TotalMilliseconds;
        var timeToSpend = frameRate.FrameDelay - spentOnFrame;
        if (timeToSpend > 0)
        {
            var nanoseconds = (long)(timeToSpend * 10000);
            frameTimer.Wait(nanoseconds);
        }
    }

    protected override void OnClose()
    {
        glContext.Dispose();
        frameRateWatcher.Stop();

        base.OnClose();
    }

    protected override unsafe bool OnMessage(nint hWnd, WindowMessage message, ulong wParam, ulong lParam)
    {
        if (framesBeforeRest <= 0)
            frameRate = DefaultFrameRate;

        framesBeforeRest = FramesBeforeRest;
        return base.OnMessage(hWnd, message, wParam, lParam);
    }

    struct FrameRate
    {
        public FrameRate(int framesPerSecond)
        {
            FramesPerSecond = framesPerSecond;
            FrameDelay = 1000d / FramesPerSecond;
            FloorFrameDelay = (int)FrameDelay;
        }

        public int FramesPerSecond { get; init; }
        public double FrameDelay { get; init; }
        public int FloorFrameDelay { get; init; }
    }
}