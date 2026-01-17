using Mossarium.Alpha.UI.Windowing.Structures;

namespace Mossarium.Alpha.UI.OpenGL;

struct Vertex
{
    public const int CoordsOffset = 0;
    public const int ColorsOffset = sizeof(ushort) + sizeof(ushort);

    public Vertex(LocationU2 location, Rgba color)
    {
        Location = location;
        Color = color;
    }

    public LocationU2 Location;
    public Rgba Color;
}