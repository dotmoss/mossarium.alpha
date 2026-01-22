using Mossarium.Alpha.UI.Windowing.Structures;
using OpenGL;
using System.Runtime.InteropServices;
using static OpenGL.Enums;

namespace Mossarium.Alpha.UI.OpenGL;

public static unsafe class GlUniformBufferRegistry
{
    public static void Initialize()
    {
        const int Count = 2;

        var buffers = stackalloc uint[Count];
        GL.GenerateBuffers(Count, buffers);

        WindowData = new GlUniformBuffer<GlslubWindowData>(buffers[0]);
    }

    public static GlUniformBuffer<GlslubWindowData> WindowData;
}

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public struct GlslubWindowData
{
    private SizeF4 size;
    private SizeF4 sizeTransformer;

    public SizeU2 Size
    {
        set
        {
            size = new SizeF4(value.Width, value.Height);
            sizeTransformer = new SizeF4(131072f / value.Width, 131072f / value.Height);
        }
    }
}