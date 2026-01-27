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

    static void DesribeIntegerAttribute(uint index, int count, DataType type, int offset)
    {
        GL.EnableVertexAttribArray(index);
        GL.VertexAttribIPointer(index, count, type, sizeof(TVertexImpl), (void*)offset);
    }
}

public unsafe interface IVertexArray : IObjectHandle
{
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

public unsafe interface ITexture : IObjectHandle 
{
    TextureTarget Target { get; }
    InternalFormat InternalFormat { get; }

    void IDisposable.Dispose()
    {
        var id = ID;
        GL.DeleteTextures(1, &id);
    }
}

public unsafe interface ITexture1D : ITexture;

public unsafe interface ITexture2D : ITexture;