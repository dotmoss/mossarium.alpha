using static OpenGL.Enums;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlUniformBuffer<T>
    where T : unmanaged
{
    static uint counter;

    public GlUniformBuffer(uint bufferId)
    {
        buffer = GlBuffer.From(bufferId, BufferType.Uniform, BufferUsage.DynamicDraw);
        buffer.Allocate(sizeof(T));
        buffer.BindToUniformBase(counter);

        counter++;
    }

    GlBuffer buffer;

    public void Write(T data)
    {
        buffer.Write(&data, sizeof(T));
    }
}