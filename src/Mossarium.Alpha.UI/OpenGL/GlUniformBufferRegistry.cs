using Mossarium.Alpha.UI.Windowing.Structures;
using OpenGL;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL;

public static unsafe class GlUniformBufferRegistry
{
    public static void Initialize()
    {
        const int Count = 2;

        var buffers = stackalloc uint[Count];
        GL.GenerateBuffers(Count, buffers);

        WindowData = new GlUniformBuffer<UbWindowData>(buffers[0]);
        RoundedRectangleData = new GlUniformBuffer<UbRoundedRectangleData>(buffers[1]);
    }

    public static GlUniformBuffer<UbWindowData> WindowData;
    public static GlUniformBuffer<UbRoundedRectangleData> RoundedRectangleData;
}

[StructLayout(LayoutKind.Explicit, Size = 16)]
public struct UbWindowData
{
    [FieldOffset(0x00)] SizeF4 size;
    [FieldOffset(0x08)] SizeF4 sizeTransformer;

    public SizeU2 Size
    {
        set
        {
            size = new SizeF4(value.Width, value.Height);
            sizeTransformer = new SizeF4(131072f / value.Width, 131072f / value.Height);
        }
    }
}

[StructLayout(LayoutKind.Explicit, Size = 48)]
public struct UbRoundedRectangleData
{
    [FieldOffset(0x00)] public LocationF4 Position;
    [FieldOffset(0x08)] public SizeF4 Size;
    [FieldOffset(0x10)] public RgbF4 Color;
    [FieldOffset(0x20)] public float CornerRadius;
}