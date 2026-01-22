namespace Mossarium.Alpha.UI.Windowing.Structures;

public struct LocationF4
{
    public LocationF4(float x, float y) => (X, Y) = (x, y);

    public float X, Y;

    public static implicit operator LocationF4((float Width, float Height) size) => new LocationF4(size.Width, size.Height);
}