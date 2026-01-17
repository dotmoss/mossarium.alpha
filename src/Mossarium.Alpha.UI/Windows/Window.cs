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

    protected override void OnWindowInitialized()
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

    uint VBO, VAO, EBO, UBO;
    protected override void OnInitializeRender()
    {
        GlslImpls.Compile();

        var vertices = stackalloc GlVertex[] 
        {
            new GlVertex((10,  10),   (255, 0, 0)),
            new GlVertex((200, 10),   (0, 255, 0)),
            new GlVertex((200, 200),  (0, 0, 255)),
            new GlVertex((10,  200),  (255, 255, 0))
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
            GL.BufferData(BufferType.Array, sizeof(GlVertex) * 4, vertices, BufferUsage.StaticDraw);

            GL.BindBuffer(BufferType.ElementArray, ebo);
            GL.BufferData(BufferType.ElementArray, sizeof(uint) * 6, indices, BufferUsage.StaticDraw);

            GL.VertexAttribPointer(0, 2, DataType.UShort, true, sizeof(GlVertex), (void*)GlVertex.CoordsOffset);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, DataType.UByte, true, sizeof(GlVertex), (void*)GlVertex.ColorsOffset);
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferType.Array, 0);
        }
        GL.BindVertexArray(0);


        GL.BindBuffer(BufferType.Uniform, ubo);
        GL.BindBufferBase(BufferType.Uniform, 0, ubo);

        var glslWindowData = new GlslWindowData
        {
            Size = ((ushort)Size.Width, (ushort)Size.Height)
        };
        GL.BufferData(BufferType.Uniform, glslWindowData, BufferUsage.DynamicDraw);

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

        GL.Clear(ClearMask.Color);

        GL.UseProgram(GlslImpls.Program.GradientRgbTriangles.ID);
        GL.BindVertexArray(VAO);
        GL.DrawElements(Mode.Triangles, 6, BUType.UInt, 0);

        GL.SwapBuffers(HandleToDeviceContext);
    }

    protected override bool OnClose()
    {
        return base.OnClose();
    }
}