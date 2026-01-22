using OpenGL;
using static OpenGL.Enums;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe interface IGlVertex<T>
    where T : unmanaged, IGlVertex<T>
{
    public static abstract void DesribeAttributes<TVertexImpl, TIndex>(GlVertexArray<TVertexImpl, TIndex> array)
        where TVertexImpl : unmanaged, IGlVertex<TVertexImpl>
        where TIndex : unmanaged;

    protected static void DesribeAttribute(uint index, int count, DataType type, bool normilize, int offset)
    {
        GL.VertexAttribPointer(index, count, type, normilize, sizeof(T), (void*)offset);
        GL.EnableVertexAttribArray(index);
    }
}