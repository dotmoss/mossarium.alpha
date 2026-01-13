namespace Mossarium.Alpha.UI.Windowing.Structures;

public struct SizeI4
{
    public SizeI4(int width, int height) => (Width, Height) = (width, height);

    public int Width, Height;

    public static implicit operator SizeI4((int Width, int Height) size) => new SizeI4(size.Width, size.Height);
}