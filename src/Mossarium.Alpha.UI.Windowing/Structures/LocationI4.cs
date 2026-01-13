namespace Mossarium.Alpha.UI.Windowing.Structures;

public struct LocationI4
{
    public LocationI4(int x, int y) => (X, Y) = (x, y);

    public int X, Y;

    public static implicit operator LocationI4((int X, int Y) location) => new LocationI4(location.X, location.Y);
}