using Mossarium.Alpha.UI.Windowing.Structures;

namespace Mossarium.Alpha.UI.OpenGL;

public struct GlVertex
{
    public const int CoordsOffset = 0;
    public const int ColorsOffset = sizeof(ushort) + sizeof(ushort);

    public GlVertex(LocationU2 location, Rgba color)
    {
        Location = location;
        Color = color;
    }

    public LocationU2 Location;
    public Rgba Color;
}