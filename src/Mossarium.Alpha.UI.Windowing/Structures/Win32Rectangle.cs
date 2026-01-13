namespace Mossarium.Alpha.UI.Windowing.Structures;

public struct Win32Rectangle
{
    public Win32Rectangle(int x, int y, int x2, int y2) => (X, Y, X2, Y2) = (x, y, x2, y2);

    public int X, Y, X2, Y2;

    public int Width { get => X2 - X; set => X2 = X + value; }
    public int Height { get => Y2 - Y; set => Y2 = Y + value; }

    public static implicit operator RectangleI4(Win32Rectangle self) => new RectangleI4
    {
        X = self.X,
        Y = self.Y,
        X2 = self.X2,
        Y2 = self.Y2
    };
}