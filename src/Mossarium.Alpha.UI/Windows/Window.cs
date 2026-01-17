using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing.Structures;
using static OpenGL.Enums;
using GL = OpenGL.GLEX;

namespace Mossarium.Alpha.UI.Windows;

public unsafe class Window : GraphicsSystemWindow
{
    public Window(string title, LocationI4 location, SizeI4 size, WindowInitialAttributes attributes = WindowInitialAttributes.None)
        : base(title, location, size, attributes)
    {

    }

    protected void InitializeWindow()
    {
        Visible = true;
    }

    void UpdateGLWindowSize()
    {
        var glslWindowData = new GlslWindowData
        {
            Size = ((ushort)Size.Width, (ushort)Size.Height)
        };
        GL.BufferSubData(BufferType.Uniform, 0, 8, &glslWindowData);
    }

    uint vertexShader, fragmentShader, shaderProgram;
    uint VBO, VAO, EBO, UBO;
    protected override void OnInitializeRender()
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

        int success = 0;
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

        GL.GetProgramiv(shaderProgram, ProgramStatusName.LinkStatus, &success);
        if (success == 0)
            throw null!;

        GL.DeleteShader(vertexShader);
        GL.DeleteShader(fragmentShader);

        var vertices = stackalloc Vertex[] 
        {
            new Vertex((10,  10),   (255, 0, 0)),
            new Vertex((200, 10),   (0, 255, 0)),
            new Vertex((200, 200),  (0, 0, 255)),
            new Vertex((10,  200),  (255, 255, 0))
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

    protected override void OnSizeChanged(SizeI4 size)
    {
        Size = size;
        UpdateGLWindowSize();

        base.OnSizeChanged(size);
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     

    protected override void OnRender()
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

    protected override bool OnClose()
    {
        return base.OnClose();
    }
}