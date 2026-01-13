using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.Windowing.Structures;

[StructLayout(LayoutKind.Explicit)]
public struct RectangleI4
{
    public RectangleI4(int x, int y, int width, int height) => (X, Y, Width, Height) = (x, y, width, height);
    public RectangleI4(LocationI4 location, SizeI4 size) => (Location, Size) = (location, size);

    [FieldOffset(0x00)] public LocationI4 Location;
    [FieldOffset(0x00)] public int X;
    [FieldOffset(0x04)] public int Y;
    [FieldOffset(0x08)] public SizeI4 Size;
    [FieldOffset(0x08)] public int Width;
    [FieldOffset(0x0C)] public int Height;

    public int X2 { get => X + Width; set => Width = value - X; }
    public int Y2 { get => Y + Height; set => Height = value - Y; }

    public static implicit operator RectangleI4((LocationI4 Location, SizeI4 Size) rectangle) => new RectangleI4(rectangle.Location, rectangle.Size);
    public static implicit operator Win32Rectangle(RectangleI4 self) => new Win32Rectangle(self.X, self.Y, self.X2, self.Y2);
}