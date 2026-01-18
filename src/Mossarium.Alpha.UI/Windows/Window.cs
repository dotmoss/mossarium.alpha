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

        UniformBuffer.Write(glslWindowData);
    }

    GlBuffer VertexBuffer, ElementsBuffer, UniformBuffer;
    GlVertexArray<GlVertex, ushort> VertexArray;
    protected override void OnInitializeRender()
    {
        GlslImpls.Compile();

        ReadOnlySpan<GlVertex> vertices = 
        [
            new GlVertex((10,  10),   (255, 0, 0)),
            new GlVertex((200, 10),   (0, 255, 0)),
            new GlVertex((200, 200),  (0, 0, 255)),
            new GlVertex((10,  200),  (255, 255, 0))
        ];

        ReadOnlySpan<ushort> indices =
        [
            0, 1, 3,
            1, 2, 3
        ];

        var vertexArray = GlVertexArray<GlVertex, ushort>.Create();

        GlBuffer vertexBuffer, elementsBuffer, uniformBuffer;
        GlBuffer.CreateMultiple(
            &vertexBuffer, BufferType.Array, BufferUsage.DynamicDraw, 
            &elementsBuffer, BufferType.ElementArray, BufferUsage.DynamicDraw,
            &uniformBuffer, BufferType.Uniform, BufferUsage.DynamicDraw
        );

        vertexArray.Bind();
        vertexBuffer.Allocate(vertices);
        elementsBuffer.Allocate(indices);

        vertexArray.DescribeAttributes();

        uniformBuffer.BindToUniformBase(0);

        var glslWindowData = new GlslWindowData
        {
            Size = ((ushort)Size.Width, (ushort)Size.Height)
        };
        uniformBuffer.Allocate(glslWindowData);

        (VertexBuffer, ElementsBuffer, UniformBuffer) = (vertexBuffer, elementsBuffer, uniformBuffer);
        VertexArray = vertexArray;
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

        GlslImpls.Program.GradientRgbTriangles.Use();
        VertexArray.Bind();
        VertexArray.DrawElements(Mode.Triangles, 0, 6);

        GL.SwapBuffers(HandleToDeviceContext);
    }

    protected override bool OnClose()
    {
        return base.OnClose();
    }
}