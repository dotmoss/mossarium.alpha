using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe interface IGlVertex<T>
    where T : unmanaged, IGlVertex<T>
{
    public static abstract void DesribeAttributes<TVertexImpl, TIndex>(GlVertexArray<TVertexImpl, TIndex> array)
        where TVertexImpl : unmanaged, IGlVertex<TVertexImpl>
        where TIndex : unmanaged;

    protected static void DesribeFloatAttribute(uint index, int count, DataType type, bool normilize, int offset)
    {
        GL.EnableVertexAttribArray(index);
        GL.VertexAttribPointer(index, count, type, normilize, sizeof(T), (void*)offset);
    }

    protected static void DesribeIntegerPointAttribute(uint index, int count, DataType type, int offset)
    {
        GL.EnableVertexAttribArray(index);
        GL.VertexAttribIPointer(index, count, type, sizeof(T), (void*)offset);
    }
}