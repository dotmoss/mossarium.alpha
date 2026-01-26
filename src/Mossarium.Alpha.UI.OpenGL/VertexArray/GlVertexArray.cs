using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

#pragma warning disable CS8618
public unsafe struct GlVertexArray : IVertexArray
{
    public GlVertexArray()
    {
        uint id;
        GL.GenerateVertexArrays(1, &id);
        ID = id;
    }

    public uint ID { get; private set; }
    public GlArrayBuffer ArrayBuffer { get; private set; }
    public IElementArrayBuffer ElementArrayBuffer { get; private set; }

    public void Bind()
    {
        GL.BindVertexArray(ID);
    }

    public GlArrayBuffer DefineVertexBuffer<TVertexImpl>()
        where TVertexImpl : unmanaged, IVertex<TVertexImpl>
    {
        Bind();
        ArrayBuffer = new GlArrayBuffer();
        ArrayBuffer.Bind();

        TVertexImpl.DesribeAttributes();
        return ArrayBuffer;
    }

    public GlElementArrayBufferU2 DefineShortIndexBuffer()
    {
        Bind();
        var elementArrayBuffer = new GlElementArrayBufferU2();
        ElementArrayBuffer = elementArrayBuffer;
        elementArrayBuffer.Bind();

        return elementArrayBuffer;
    }

    public GlElementArrayBufferU4 DefineIntegerIndexBuffer()
    {
        Bind();
        var elementArrayBuffer = new GlElementArrayBufferU4();
        ElementArrayBuffer = elementArrayBuffer;
        elementArrayBuffer.Bind();

        return elementArrayBuffer;
    }
}