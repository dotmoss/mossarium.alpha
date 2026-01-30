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

    public void Bind()
    {
        GL.BindVertexArray(ID);
    }

    public GlArrayBuffer DefineVertexBuffer<TVertexImpl>()
        where TVertexImpl : unmanaged, IVertex<TVertexImpl>
    {
        Bind();
        var arrayBuffer = new GlArrayBuffer();
        arrayBuffer.Bind();

        TVertexImpl.DescribeAttributes();
        return arrayBuffer;
    }

    public GlElementArrayBufferU2 DefineShortIndexBuffer()
    {
        Bind();
        var elementArrayBuffer = new GlElementArrayBufferU2();
        elementArrayBuffer.Bind();

        return elementArrayBuffer;
    }

    public GlElementArrayBufferU4 DefineIntegerIndexBuffer()
    {
        Bind();
        var elementArrayBuffer = new GlElementArrayBufferU4();
        elementArrayBuffer.Bind();

        return elementArrayBuffer;
    }
}