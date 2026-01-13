using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing;
using Mossarium.Alpha.UI.Windowing.Structures;
using System.Diagnostics;
using WindowsOS;
using WindowsOS.Utils;
using static OpenGL.Enums;
using GL = OpenGL.GLEX;

namespace Mossarium.Alpha.UI;

public unsafe class Window : SystemWindow
{
    public static Rgb TransparentColor = (56, 30, 12);
    const int DefaultFramesPerSecond = 30;
    const int SecondsBeforeRest = 20;
    const int FramesBeforeRest = SecondsBeforeRest * DefaultFramesPerSecond;
    static readonly FrameRateInfo DefaultFrameRateInfo = new FrameRateInfo(DefaultFramesPerSecond);
    static readonly FrameRateInfo InRestFrameRateInfo = new FrameRateInfo(5);

    public Window(string title, LocationI4 location, SizeI4 size, WindowInitialAttributes attributes = WindowInitialAttributes.None)
        : base(title, location, size)
    {
        SetWindowAttributes(attributes);
    }

    FrameRateInfo frameRateInfo = DefaultFrameRateInfo;
    int framesBeforeRest;

    void SetWindowAttributes(WindowInitialAttributes attributes)
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

    protected void InitializeWindow()
    {
        Visible = true;
    }

    protected void EnableLayering()
    {
        const int LWA_COLORKEY = 1;

        ExStyle |= WindowExStyles.Layered;
        User32.SetLayeredWindowAttributes(Handle, TransparentColor.Win32Value, default, LWA_COLORKEY);
    }

    protected void DisableLayering()
    {
        ExStyle &= ~WindowExStyles.Layered;
        User32.SetLayeredWindowAttributes(Handle, default, default, default);
    }

    uint vertexShader, fragmentShader, shaderProgram;
    uint VBO, VAO, EBO, UBO;
    protected void OnGLInitialized()
    {
        var vertexShaderSource = @"
#version 420 core
layout (std140, binding = 0) uniform WindowData {
    vec2 winSizeT;
};
layout (location = 0) in vec2 aPos;
layout (location = 1) in vec3 aColor;
out vec3 ourColor;
void main()
{
    gl_Position = vec4(aPos.x * winSizeT.x - 1, aPos.y * winSizeT.y - 1, 0.0, 1.0);
    ourColor = aColor;
}"u8;

        var fragmentShaderSource = @"
#version 420 core
in vec3 ourColor;
out vec4 FragColor;
void main()
{
    FragColor = vec4(ourColor, 1.0);
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

        var vertices = stackalloc Vertex[] 
        {
            new Vertex(10,  10,   255, 0, 0),
            new Vertex(200, 10,   0, 255, 0),
            new Vertex(200, 200,  0, 0, 255),
            new Vertex(10,  200,  255, 255, 0)
        };

        var indices = stackalloc uint[] 
        {
            0, 1, 3,
            1, 2, 3
        };

        uint vbo, vao, ebo, ubo;
        GL.GenerateVertexArrays(1, &vao);
        GL.GenerateBuffers(1, &vbo);
        GL.GenerateBuffers(1, &ebo);
        GL.GenerateBuffers(1, &ubo);

        GL.BindVertexArray(vao);
        {
            GL.BindBuffer(BufferType.Array, vbo);
            GL.BufferData(BufferType.Array, sizeof(Vertex) * 4, vertices, BufferUsage.StaticDraw);

            GL.BindBuffer(BufferType.ElementArray, ebo);
            GL.BufferData(BufferType.ElementArray, sizeof(uint) * 6, indices, BufferUsage.StaticDraw);

            GL.VertexAttribPointer(0, 2, DataType.UShort, true, sizeof(Vertex), (void*)Vertex.CoordsOffset);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, DataType.UByte, true, sizeof(Vertex), (void*)Vertex.ColorsOffset);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferType.Array, 0);
        }
        GL.BindVertexArray(0);


        GL.BindBuffer(BufferType.Uniform, ubo);

        var glslWindowData = new GlslWindowData
        {
            Size = ((ushort)Size.Width, (ushort)Size.Height)
        };
        GL.BufferData(BufferType.Uniform, 8, &glslWindowData, BufferUsage.DynamicDraw);

        GL.BindBufferBase(BufferType.Uniform, 0, ubo);

        GL.BindBuffer(BufferType.Uniform, 0);

        (VBO, VAO, EBO, UBO) = (vbo, vao, ebo, ubo);
    }

    struct Vertex
    {
        public const int CoordsOffset = 0;
        public const int ColorsOffset = sizeof(ushort) + sizeof(ushort);

        public Vertex(ushort x, ushort y, byte red, byte green, byte blue)
        {
            X = x;
            Y = y;
            Red = red;
            Green = green;
            Blue = blue;
        }

        public ushort X, Y;
        public byte Red, Green, Blue;
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
        GL.Enable(Cap.Blend);
        GL.BlendFunc(FactorEnum.SrcAlpha, FactorEnum.OneMinusSrcAlpha);
        GL.ClearColor(0f, 0f, 0f, 0f);

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
                frameRateInfo = InRestFrameRateInfo;

            OnRender();

            var spentOnFrame = frameRateWatcher.Elapsed.TotalMilliseconds;

            var timeToSpend = frameRateInfo.FrameDelay - spentOnFrame;
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
            frameRateInfo = DefaultFrameRateInfo;

        framesBeforeRest = FramesBeforeRest;
        return base.OnMessage(hWnd, message, wParam, lParam);
    }

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
}