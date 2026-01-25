
using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlUniformBuffer<T>
    where T : unmanaged
{
    public GlUniformBuffer(uint bufferId)
    {
        buffer = GlBuffer.From(bufferId, BufferType.Uniform, BufferUsage.DynamicDraw);
        buffer.Allocate(sizeof(T));
        buffer.BindToUniformBase(BufferCounter.Value);

        BufferCounter.Value++;
    }

    GlBuffer buffer;

    public void Write(T data)
    {
        buffer.Write(&data, sizeof(T));
    }
}

file static class BufferCounter 
{
    public static uint Value;
}