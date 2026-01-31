using DebugProfiler;
using Mossarium.Alpha.UI.Managers;
using Mossarium.Alpha.UI.OpenGL;
using Mossarium.Alpha.UI.OpenGL.Bins;
using Mossarium.Alpha.UI.Windowing;
using Mossarium.Alpha.UI.Windowing.Structures;
using OpenGL;
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

    protected override void OnSizeChanged(int width, int height)
    {
        GL.Viewport(0, 0, width, height);
    }

    protected virtual void OnRendererInitialized()
    {
        vertexArray = new GlVertexArray();
        vertexBuffer = vertexArray.DefineVertexBuffer<SpriteVertex>();

        ReadOnlySpan<SpriteVertex> vertexes =
        [
            new SpriteVertex((50, 50), (100, 100), 0),
            new SpriteVertex((150, 150), (16, 16), 2000)
        ];
        vertexBuffer.Allocate(vertexes);

        atlasBuffer = new GlTextureBuffer();
        atlasBuffer.Allocate(2048 * 2048 * 4);
        atlasRgb8View = atlasBuffer.DefineTextureRgb8();
        atlas = Atlas<GlTextureBuffer>.Create(atlasBuffer);

        var bytes = new byte[2048 * 2048 * 4];
        int counter = 0;
        for (var i = 0; i < 2048 * 2048 * 4; i += 4)
        {
            bytes[i] = (byte)(counter % byte.MaxValue);
            bytes[i + 1] = (byte)(counter % byte.MaxValue);
            bytes[i + 2] = (byte)(counter % byte.MaxValue);
            bytes[i + 3] = byte.MaxValue;

            counter++;
        }

        fixed (byte* bytesPointer = bytes)
            atlasBuffer.Write(bytesPointer, 2048 * 2048 * 4, 0);

        vertexShader = new GlShader(ShaderType.Vertex,
@"#version 430 core

layout (std140, binding = 0) uniform WindowData
{
    vec2 winSize;
    vec2 winSizeT;
};

layout (location = 0) in uint inPackPosition;
layout (location = 1) in uint inPackSize;
layout (location = 2) in uint inTexOffset;

out outVS
{
    uint packSize;
    float texOffset;
} outVs;

void main() 
{
    vec2 position = vec2(float(inPackPosition & 0xFFFFU), float(inPackPosition >> 16));
    vec2 normPosition = position / winSize * 2.0 - 1.0;
    gl_Position = vec4(normPosition, 0.0, 1.0);

    outVs.packSize = inPackSize;
    outVs.texOffset = float(inTexOffset);
}"u8
        );

        geometryShader = new GlShader(ShaderType.Geometry,
@"#version 430 core

layout (std140, binding = 0) uniform WindowData
{
    vec2 winSize;
    vec2 winSizeT;
};

in outVS
{
    uint packSize;
    float texOffset;
} inGs[];

layout (points) in;
layout (triangle_strip, max_vertices = 4) out;

out vec2 fTexCoords;
 
void main() 
{
    vec2 positionStart = gl_in[0].gl_Position.xy;
    uint packSize = inGs[0].packSize;
    vec2 size = vec2(float(packSize & 0xFFFFU), float(packSize >> 16));
    vec2 normSize = size / winSize * 2.0;
    float texStart = inGs[0].texOffset;

    vec2 positionEnd = positionStart + normSize;

    float texLength = size.x * size.y;
    float texEnd = texStart + texLength;
    
    gl_Position = vec4(positionStart, 0.0, 1.0);
    fTexCoords = vec2(texEnd, 0);
    EmitVertex();

    gl_Position = vec4(positionEnd.x, positionStart.y, 0.0, 1.0);
    fTexCoords = vec2(texEnd, size.x);
    EmitVertex();

    gl_Position = vec4(positionStart.x, positionEnd.y, 0.0, 1.0);
    fTexCoords = vec2(texStart, 0);
    EmitVertex();

    gl_Position = vec4(positionEnd, 0.0, 1.0);
    fTexCoords = vec2(texStart, size.x);
    EmitVertex();

    EndPrimitive();
}"u8
        );

        fragmentShader = new GlShader(ShaderType.Fragment,
@"#version 430 core

layout(binding = 0) uniform samplerBuffer atlas;

in vec2 fTexCoords;

out vec4 FragColor;

void main() 
{
    FragColor = texelFetch(atlas, int(fTexCoords.x + fTexCoords.y));
}"u8
        );

        program = new GlProgram(vertexShader, geometryShader, fragmentShader);

        emptyVertexArray = new GlVertexArray();
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 0x0C)]
    struct SpriteVertex : IVertex<SpriteVertex>
    {
        public SpriteVertex(LocationU2 position, SizeU2 size, uint imageOffset)
        {
            Position = position;
            Size = size;
            ImageOffset = imageOffset;
        }

        public LocationU2 Position;
        public SizeU2 Size;
        public uint ImageOffset;

        static void IVertex<SpriteVertex>.DescribeAttributes()
        {
            IVertex<SpriteVertex>.DesribeIntegerAttribute(0, 1, DataType.UInt, 0);
            IVertex<SpriteVertex>.DesribeIntegerAttribute(1, 1, DataType.UInt, sizeof(LocationU2));
            IVertex<SpriteVertex>.DesribeIntegerAttribute(2, 1, DataType.UInt, sizeof(LocationU2) + sizeof(SizeU2));
        }
    }

    GlVertexArray vertexArray, emptyVertexArray;
    GlArrayBuffer vertexBuffer;
    GlTextureBuffer atlasBuffer;
    GlBufferTextureRgb8 atlasRgb8View;
    Atlas<GlTextureBuffer>* atlas;
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