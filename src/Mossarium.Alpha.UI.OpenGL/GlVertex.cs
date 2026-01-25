using Mossarium.Alpha.UI.Windowing.Structures;
using OpenGL;

namespace Mossarium.Alpha.UI.OpenGL;

public unsafe struct GlVertex : IGlVertex<GlVertex>
{
    public GlVertex(LocationU2 location, Rgb color)
    {
        Location = location;
        Color = color;
    }

    public LocationU2 Location;
    public Rgb Color;

    public static void DesribeAttributes<TVertexImpl, TIndex>(GlVertexArray<TVertexImpl, TIndex> array) 
        where TVertexImpl : unmanaged, IGlVertex<TVertexImpl>
        where TIndex : unmanaged
    {
        IGlVertex<GlVertex>.DesribeFloatAttribute(0, 2, DataType.UShort, true, 0);
        IGlVertex<GlVertex>.DesribeFloatAttribute(1, 3, DataType.UByte, true, sizeof(LocationU2));
    }
}