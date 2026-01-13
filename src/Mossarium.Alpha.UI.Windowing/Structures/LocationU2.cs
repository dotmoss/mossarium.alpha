namespace Mossarium.Alpha.UI.Windowing.Structures;

public struct LocationU2
{
    public LocationU2(ushort x, ushort y) => (X, Y) = (x, y);

    public ushort X, Y;

    public static implicit operator LocationU2((ushort X, ushort Y) location) => new LocationU2(location.X, location.Y);
}