using DebugProfiler;
using Mossarium.Alpha.UI.Managers;
using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing;
using Mossarium.Alpha.UI.Windowing.Structures;
using OpenGL;
using System.Runtime.InteropServices;
using WindowsOS;
using static OpenGL.Enums;

namespace Mossarium.Alpha.UI;

public unsafe class Window : SystemWindow
{
    public Window(string title, LocationI4 location, SizeI4 size, WindowInitialAttributes attributes = WindowInitialAttributes.None)
        : base(title, location, size, attributes)
    {

    }

    public WindowManager.PrivateWindowManagerData PrivateWindowManagerData;

    protected override void OnMouseLeftDown()
    {
        SendMessage(WindowMessage.NcLButtonDown, 0x02, 0x00);
    }

    protected virtual void OnRendererInitialized()
    {
        vertexArray = GlVertexArray<SpriteVertex, ushort>.Create();
        vertexBuffer = GlBuffer.Create(BufferType.Array, BufferUsage.DynamicDraw);

        vertexArray.Bind();
        vertexBuffer.Bind();

        ReadOnlySpan<SpriteVertex> vertexes =
        [
            new SpriteVertex((50.0f, 50.0f), (50f, 50f), 0.0f),
            new SpriteVertex((150.0f, 150.0f), (50f, 50f), 0.5f)
        ];
        vertexBuffer.Allocate(vertexes);

        vertexArray.DescribeAttributes();

        atlas = GlTexture1D.Create();
        atlas.Allocate(2048 * 2048);

        vertexShader = new GlShader(ShaderType.Vertex,
@"#version 430 core

layout (location = 0) in vec2 position;
layout (location = 1) in vec2 size;
layout (location = 2) in float imageOffset;

out VS_OUT {
    vec2 size;
    float imageOffset;
} vs_out;

void main() {
    gl_Position = vec4(position, 0.0, 1.0); 
    vs_out.size = size;
    vs_out.imageOffset = imageOffset;
}"u8
        );

        geometryShader = new GlShader(ShaderType.Geomenty,
@"#version 430 core

layout (std140, binding = 0) uniform WindowData {
    vec2 winSize;
    vec2 winSizeT;
};

in VS_OUT {
    vec2 size;
    float imageOffset;
} gs_in[];

layout (points) in;
layout (triangle_strip, max_vertices = 4) out;

out float fTexCoord;

void main() {
    vec2 pos = gl_in[0].gl_Position.xy;
    vec2 size = gs_in[0].size;
    float offset = gs_in[0].imageOffset;

    float uStart = offset;
    float uEnd = uStart + size.x / 2048.0 * size.y / 2048.0;
    
    gl_Position = vec4(pos.x / winSize.x * 2.0 - 1.0, pos.y / winSize.y * 2.0 - 1.0, 0.0, 1.0);
    fTexCoord = uStart;
    EmitVertex();

    gl_Position = vec4((pos.x + size.x) / winSize.x * 2.0 - 1.0, pos.y / winSize.y * 2.0 - 1.0, 0.0, 1.0);
    fTexCoord = uEnd;
    EmitVertex();

    gl_Position = vec4(pos.x / winSize.x * 2.0 - 1.0, (pos.y + size.y) / winSize.y * 2.0 - 1.0, 0.0, 1.0);
    fTexCoord = uStart;
    EmitVertex();

    gl_Position = vec4((pos.x + size.x) / winSize.x * 2.0 - 1.0, (pos.y + size.y) / winSize.y * 2.0 - 1.0, 0.0, 1.0);
    fTexCoord = uEnd;
    EmitVertex();

    EndPrimitive();
}"u8
        );

        fragmentShader = new GlShader(ShaderType.Fragment,
@"#version 430 core

in float fTexCoord;
out vec4 FragColor;

layout(binding = 0) uniform sampler1D atlas;

void main() {
    FragColor = texture(atlas, fTexCoord);
}"u8
        );

        program = new GlProgram(vertexShader, geometryShader, fragmentShader);
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    struct SpriteVertex : IGlVertex<SpriteVertex>
    {
        public SpriteVertex(LocationF4 position, SizeF4 size, float imageOffset)
        {
            Position = position;
            Size = size;
            ImageOffset = imageOffset;
        }

        public LocationF4 Position;
        public SizeF4 Size;
        public float ImageOffset;

        public static void DesribeAttributes<TVertexImpl, TIndex>(GlVertexArray<TVertexImpl, TIndex> array)
            where TVertexImpl : unmanaged, IGlVertex<TVertexImpl>
            where TIndex : unmanaged
        {
            IGlVertex<SpriteVertex>.DesribeFloatAttribute(0, 2, DataType.Float, false, 0);
            IGlVertex<SpriteVertex>.DesribeFloatAttribute(1, 2, DataType.Float, false, sizeof(LocationF4));
            IGlVertex<SpriteVertex>.DesribeFloatAttribute(2, 1, DataType.Float, false, sizeof(LocationF4) + sizeof(SizeF4));
        }
    }

    GlVertexArray<SpriteVertex, ushort> vertexArray;
    GlBuffer vertexBuffer;
    GlTexture1D atlas;
    GlShader vertexShader, geometryShader, fragmentShader;
    GlProgram program;

    protected virtual void OnRender()
    {
        OpenGlManager.MakeWindowCurrent(this);
        GL.Clear(ClearMask.Color);

        var ubWindowData = new UbWindowData
        {
            Size = ((ushort)Size.Width, (ushort)Size.Height)
        };
        GlUniformBufferRegistry.WindowData.Write(ubWindowData);

        program.Use();

        atlas.Active(0);

        GL.DrawArrays(Mode.Points, 0, 2);

        GlPrograms.TransparentWindowCorners.Use();
        GL.DrawArrays(Mode.Triangles, 0, 12);

        GDI32.SwapBuffers(DeviceContextHandle);
    }

    public new void Dispose()
    {
        WindowManager.FinalizeWindow(this);
        base.Dispose();
    }
    
    static Window()
    {
        Profiler.Register<ProfileStage>("UserInterface");
    }

    public static new class Dispatcher
    {
        public static void OnRender(Window window) => window.OnRender();
        public static void OnRendererInitialized(Window window) => window.OnRendererInitialized();
    }
}