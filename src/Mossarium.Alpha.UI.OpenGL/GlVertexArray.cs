using OpenGL;
using static OpenGL.Enums;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlVertexArray<TVertex, TIndex>
    where TVertex : unmanaged, IGlVertex<TVertex>
    where TIndex : unmanaged
{
    public uint ID { get; private set; }
    public BUType IndexType 
    {
        get => this switch
        {
            GlVertexArray<TVertex, byte> value => BUType.UByte,
            GlVertexArray<TVertex, short> value => BUType.UShort,
            GlVertexArray<TVertex, ushort> value => BUType.UShort,
            GlVertexArray<TVertex, int> value => BUType.UInt,
            GlVertexArray<TVertex, uint> value => BUType.UInt,
            _ => throw null!
        };
    }

    public void Bind()
    {
        GL.BindVertexArray(ID);
    }

    public void DrawElements(Mode mode, int offset, int count)
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