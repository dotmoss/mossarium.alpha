namespace Mossarium.Alpha.UI.Windowing.Structures;

public struct SizeF4
{
    public SizeF4(float width, float height) => (Width, Height) = (width, height);

    public float Width, Height;

    public static implicit operator SizeF4((float Width, float Height) size) => new SizeF4(size.Width, size.Height);
}