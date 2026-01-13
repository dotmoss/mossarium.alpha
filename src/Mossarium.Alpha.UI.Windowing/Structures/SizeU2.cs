namespace Mossarium.Alpha.UI.Windowing.Structures;

public struct SizeU2
{
    public SizeU2(ushort width, ushort height) => (Width, Height) = (width, height);

    public ushort Width, Height;

    public static implicit operator SizeU2((ushort Width, ushort Height) size) => new SizeU2(size.Width, size.Height);
}