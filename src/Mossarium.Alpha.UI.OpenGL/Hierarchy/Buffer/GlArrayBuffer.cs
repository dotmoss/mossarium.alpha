using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

#pragma warning disable CS0649
public unsafe struct GlArrayBuffer : IBufferHandle
{
    public GlArrayBuffer()
    {
        uint id;
        GL.GenerateBuffers(1, &id);
        ID = id;
    }

    public uint ID { get; private set; }
    public BufferType Type => BufferType.Array;
}