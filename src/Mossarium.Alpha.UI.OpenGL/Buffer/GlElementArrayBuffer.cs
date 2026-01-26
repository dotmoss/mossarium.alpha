using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

#pragma warning disable CS0649
public unsafe struct GlElementArrayBufferU2 : IElementArrayBuffer
{
    public GlElementArrayBufferU2()
    {
        uint id;
        GL.GenerateBuffers(1, &id);
        ID = id;
    }

    public uint ID { get; private set; }
    public BufferType Type => BufferType.Array;
    public ElementIndexType IndexType => ElementIndexType.UShort;
}

public unsafe struct GlElementArrayBufferU4 : IElementArrayBuffer
{
    public GlElementArrayBufferU4()
    {
        uint id;
        GL.GenerateBuffers(1, &id);
        ID = id;
    }

    public uint ID { get; private set; }
    public BufferType Type => BufferType.Array;
    public ElementIndexType IndexType => ElementIndexType.UInt;
}