using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlVertexArray<TVertex, TIndex>
    where TVertex : unmanaged, IGlVertex<TVertex>
    where TIndex : unmanaged
{
    public GlVertexArray() => throw null!;

    public uint ID { get; private set; }
    public ElementIndexType IndexType 
    {
        get => this switch
        {
            GlVertexArray<TVertex, byte> value => ElementIndexType.UByte,
            GlVertexArray<TVertex, short> value => ElementIndexType.UShort,
            GlVertexArray<TVertex, ushort> value => ElementIndexType.UShort,
            GlVertexArray<TVertex, int> value => ElementIndexType.UInt,
            GlVertexArray<TVertex, uint> value => ElementIndexType.UInt,
            _ => throw null!
        };
    }

    public void Bind()
    {
        GL.BindVertexArray(ID);
    }

    public void DrawElements(DrawMode mode, int offset, int count)
    {
        GL.DrawElements(mode, count, IndexType, offset);
    }

    public void DescribeAttributes()
    {
        TVertex.DesribeAttributes(this);
    }

    public void Dispose()
    {
        var id = ID;
        GL.DeleteVertexArray(1, &id);
    }

    public static GlVertexArray<TVertex, TIndex> Create()
    {
        GlVertexArray<TVertex, TIndex> buffer;
        GL.GenerateVertexArrays(1, (uint*)&buffer);
        return buffer;
    }
}