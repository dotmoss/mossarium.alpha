using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public interface IObjectHandle : IDisposable
{
    uint ID { get; }
}

public unsafe interface INonFunctionalBufferHandle : IObjectHandle
{
    BufferType Type { get; }

    void IDisposable.Dispose()
    {
        var id = ID;
        GL.DeleteBuffers(1, &id);
    }
}

public unsafe interface IBufferHandle : INonFunctionalBufferHandle;

public unsafe interface IVertex<TVertexImpl>
    where TVertexImpl : unmanaged, IVertex<TVertexImpl>
{
    static abstract void DesribeAttributes();

    static void DesribeFloatAttribute(uint index, int count, DataType type, bool normilize, int offset)
    {
        GL.EnableVertexAttribArray(index);
        GL.VertexAttribPointer(index, count, type, normilize, sizeof(TVertexImpl), (void*)offset);
    }

    static void DesribeIntegerPointAttribute(uint index, int count, DataType type, int offset)
    {
        GL.EnableVertexAttribArray(index);
        GL.VertexAttribIPointer(index, count, type, sizeof(TVertexImpl), (void*)offset);
    }
}

public unsafe interface IVertexArray : IObjectHandle
{
    GlArrayBuffer ArrayBuffer { get; }
    IElementArrayBuffer ElementArrayBuffer { get; }

    void IDisposable.Dispose()
    {
        var id = ID;
        GL.DeleteVertexArray(1, &id);
    }
}

public interface IElementArrayBuffer : IBufferHandle
{
    ElementIndexType IndexType { get; }
}