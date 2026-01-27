using DebugProfiler;
using Mossarium.Alpha.UI.Managers;
using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.Windowing;
using Mossarium.Alpha.UI.Windowing.Structures;
using OpenGL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WindowsOS;

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
        vertexArray = new GlVertexArray();
        vertexBuffer = vertexArray.DefineVertexBuffer<SpriteVertex>();

        ReadOnlySpan<SpriteVertex> vertexes =
        [
            new SpriteVertex((50.0f, 50.0f), (50f, 50f), 100),
            new SpriteVertex((150.0f, 150.0f), (50f, 50f), 200)
        ];
        vertexBuffer.Allocate(vertexes);

        atlas = new GlTextureBuffer();
        atlas.Allocate(2048 * 2048 * 4);
        atlasRgb8View = atlas.DefineTextureRgb8();

        var bytes = new byte[2048 * 2048 * 4];
        int counter = 0;
        for (var i = 0; i < 2048 * 2048 * 4; i += 4)
        {
            bytes[i] = (byte)(counter % byte.MaxValue);
            bytes[i + 1] = (byte)(counter % byte.MaxValue / 2);
            bytes[i + 2] = (byte)(counter % byte.MaxValue / 4);
            bytes[i + 3] = byte.MaxValue;

            counter++;
        }

        fixed (byte* bytesPointer = bytes)
            atlas.Write(bytesPointer, 2048 * 2048, 0);

        vertexShader = new GlShader(ShaderType.Vertex,
@"#version 430 core

layout (location = 0) in vec2 position;
layout (location = 1) in vec2 size;
layout (location = 2) in uint imageOffset;

out VS_OUT 
{
    vec2 size;
    flat uint imageOffset;
} vs_out;

void main() 
{
    gl_Position = vec4(position, 0.0, 1.0); 
    vs_out.size = size;
    vs_out.imageOffset = imageOffset;
}"u8
        );

        geometryShader = new GlShader(ShaderType.Geometry,
@"#version 430 core

layout (std140, binding = 0) uniform WindowData
{
    vec2 winSize;
    vec2 winSizeT;
};

in VS_OUT 
{
    vec2 size;
    flat uint imageOffset;
} gs_in[];

layout (points) in;
layout (triangle_strip, max_vertices = 4) out;

flat out uint fTexIndex;

void main() 
{
    vec2 pos = gl_in[0].gl_Position.xy;
    vec2 size = gs_in[0].size;
    uint offset = gs_in[0].imageOffset;

    uint uStart = offset;
    uint uEnd = uStart + uint(size.x * size.y);
    
    gl_Position = vec4(pos.x / winSize.x * 2.0 - 1.0, pos.y / winSize.y * 2.0 - 1.0, 0.0, 1.0);
    fTexIndex = uStart;
    EmitVertex();

    gl_Position = vec4((pos.x + size.x) / winSize.x * 2.0 - 1.0, pos.y / winSize.y * 2.0 - 1.0, 0.0, 1.0);
    fTexIndex = uEnd;
    EmitVertex();

    gl_Position = vec4(pos.x / winSize.x * 2.0 - 1.0, (pos.y + size.y) / winSize.y * 2.0 - 1.0, 0.0, 1.0);
    fTexIndex = uStart;
    EmitVertex();

    gl_Position = vec4((pos.x + size.x) / winSize.x * 2.0 - 1.0, (pos.y + size.y) / winSize.y * 2.0 - 1.0, 0.0, 1.0);
    fTexIndex = uEnd;
    EmitVertex();

    EndPrimitive();
}"u8
        );

        fragmentShader = new GlShader(ShaderType.Fragment,
@"#version 430 core

flat in uint fTexIndex;
out vec4 FragColor;

layout(binding = 0) uniform samplerBuffer atlas;

void main() {
    FragColor = texelFetch(atlas, int(fTexIndex));
}"u8
        );

        program = new GlProgram(vertexShader, geometryShader, fragmentShader);

        emptyVertexArray = new GlVertexArray();
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x14)]
    struct SpriteVertex : IVertex<SpriteVertex>
    {
        public SpriteVertex(LocationF4 position, SizeF4 size, uint imageOffset)
        {
            Position = position;
            Size = size;
            ImageOffset = imageOffset;
        }

        public LocationF4 Position;
        public SizeF4 Size;
        public uint ImageOffset;

        static void IVertex<SpriteVertex>.DesribeAttributes()
        {
            IVertex<SpriteVertex>.DesribeFloatAttribute(0, 2, DataType.Float, false, 0);
            IVertex<SpriteVertex>.DesribeFloatAttribute(1, 2, DataType.Float, false, sizeof(LocationF4));
            IVertex<SpriteVertex>.DesribeIntegerPointAttribute(2, 1, DataType.UInt, sizeof(LocationF4) + sizeof(SizeF4));
        }
    }

    GlVertexArray vertexArray, emptyVertexArray;
    GlArrayBuffer vertexBuffer;
    GlTextureBuffer atlas;
    GlBufferTextureRgb8 atlasRgb8View;
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
        atlasRgb8View.Active(0);
        vertexArray.Bind();
        GL.DrawArrays(DrawMode.Points, 0, 2);

        GlPrograms.TransparentWindowCorners.Use();
        emptyVertexArray.Bind();
        GL.DrawArrays(DrawMode.Triangles, 0, 12);

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