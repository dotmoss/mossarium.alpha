using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing.Structures;
using WindowsOS;
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

    static GlBuffer VertexBuffer, ElementsBuffer, UniformBuffer;
    static GlVertexArray<GlVertex, ushort> VertexArray;
    static void OnGLInitialized()
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

        var glslWindowData = new GlslWindowData();
        uniformBuffer.Allocate(glslWindowData);

        (VertexBuffer, ElementsBuffer, UniformBuffer) = (vertexBuffer, elementsBuffer, uniformBuffer);
        VertexArray = vertexArray;
    }

    protected override void OnSizeChanged(SizeI4 size)
    {
        Size = size;

        base.OnSizeChanged(size);
    }

    protected override void OnRender()
    {
        var glslWindowData = new GlslWindowData
        {
            Size = ((ushort)Size.Width, (ushort)Size.Height)
        };
        UniformBuffer.Write(glslWindowData);

        GL.Enable(Cap.Blend);
        GL.BlendFunc(FactorEnum.SrcAlpha, FactorEnum.OneMinusSrcAlpha);

        GL.Clear(ClearMask.Color);

        GlslImpls.Program.AP.Use();

        GL.Uniform(0, 160.0f, 120.0f);
        GL.Uniform(1, 150.0f, 120.0f);
        GL.Uniform(2, 20.0f);
        GL.Uniform(3, 1.0f, 0.0f, 0.0f);
        GL.DrawArrays(Mode.TriangleStrip, 0, 4);

        //GlslImpls.Program.GradientRgbTriangles.Use();
        //VertexArray.Bind();
        //VertexArray.DrawElements(Mode.Triangles, 0, 6);

        GDI32.SwapBuffers(HandleToDeviceContext);
    }

    static Window()
    {
        WindowGLContext.Initialized += OnGLInitialized;
    }
}