using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing;
using System.Diagnostics;
using WindowsOS;
using WindowsOS.Utils;
using static OpenGL.Enums;
using GL = OpenGL.GLEX;

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

    }

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

    uint vertexShader, fragmentShader, shaderProgram;
    uint VBO, VAO, EBO;
    protected void OnGLInitialized()
    {
        var vertexShaderSource = @"
#version 330 core
layout (location = 0) in vec3 aPos;
void main()
{
   gl_Position = vec4(aPos.x, aPos.y, aPos.z, 1.0);
}"u8;

        var fragmentShaderSource = @"
#version 330 core
out vec4 FragColor;
void main()
{
    FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}"u8;
                
        vertexShader = GL.CreateShader(ShaderType.Vertex);
        GL.ShaderSource(vertexShader, vertexShaderSource);
        GL.CompileShader(vertexShader);

        int success;
        GL.GetShaderiv(vertexShader, ShaderStatusName.CompileStatus, &success);
        if (success == 0)
            throw null!;

        fragmentShader = GL.CreateShader(ShaderType.Fragment);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        GL.CompileShader(fragmentShader);

        GL.GetShaderiv(fragmentShader, ShaderStatusName.CompileStatus, &success);
        if (success == 0)
            throw null!;

        shaderProgram = GL.CreateProgram();
        GL.AttachShader(shaderProgram, vertexShader);
        GL.AttachShader(shaderProgram, fragmentShader);
        GL.LinkProgram(shaderProgram);

        GL.GetProgramiv(fragmentShader, ProgramStatusName.LinkStatus, &success);
        if (success == 0)
            throw null!;

        // remove shaders

        float[] vertices = {
             0.5f,  0.4f, 0.0f,
             0.5f, -0.7f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.6f,  0.5f, 0.0f
        };

        uint[] indices = {
            0, 1, 3,
            1, 2, 3
        };

        uint vbo, vao, ebo;
        GL.GenerateVertexArrays(1, &vao);
        GL.GenerateBuffers(1, &vbo);
        GL.GenerateBuffers(1, &ebo);

        GL.BindVertexArray(vao);

        GL.BindBuffer(BufferType.Array, vbo);
        GL.BufferData(BufferType.Array, vertices, BufferUsage.StaticDraw);

        GL.BindBuffer(BufferType.ElementArray, ebo);
        GL.BufferData(BufferType.ElementArray, indices, BufferUsage.StaticDraw);

        GL.VertexAttribPointer(0, 3, DataType.Float, false, 3 * sizeof(float), null);
        GL.EnableVertexAttribArray(0);

        GL.BindBuffer(BufferType.Array, 0);

        GL.BindVertexArray(0);

        (VBO, VAO, EBO) = (vbo, vao, ebo);
    }

    protected virtual void OnInitialized() { }

    protected override void OnMessageLoopStarted()
    {
        var thread = new Thread(StartRenderLoop)
        {
            Name = $"UI Render Loop Thread 0x{Handle:X}"
        };
        
        thread.Start();
    }

    protected virtual void OnRender()
    {
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

        GL.Clear(ClearMask.Color);

        GL.UseProgram(shaderProgram);
        GL.BindVertexArray(VAO);
        GL.DrawElements(Mode.Triangles, 6, BUType.UInt, 0);

        GL.SwapBuffers(HandleToDeviceContext);
    }

    void StartRenderLoop()
    {
        var frameRateWatcher = new Stopwatch();
        using var frameTimer = new HighAccuracyWaitableTimer();
        using var glContext = new WindowGLContext(HandleToDeviceContext);

        OnGLInitialized();

        while (isRunning)
        {
            frameRateWatcher.Restart();

            framesBeforeRest--;
            if (framesBeforeRest == 0)
                frameRate = InRestFrameRate;

            OnRender();

            var spentOnFrame = frameRateWatcher.Elapsed.TotalMilliseconds;
            var timeToSpend = frameRate.FrameDelay - spentOnFrame;
            if (timeToSpend > 0)
            {
                var nanoseconds = (long)(timeToSpend * 10000);
                frameTimer.Wait(nanoseconds);
            }
        }

        frameRateWatcher.Stop();
    }

    protected override bool OnClose()
    {
        return base.OnClose();
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