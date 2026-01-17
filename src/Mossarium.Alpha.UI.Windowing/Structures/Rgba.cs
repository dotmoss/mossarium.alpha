namespace Mossarium.Alpha.UI.Windowing.Structures;

public struct Rgba
{
    public Rgba(int r, int g, int b, int a) : this((byte)r, (byte)g, (byte)b, (byte)a) { }

    public Rgba(byte r, byte g, byte b, byte a) => (R, G, B, A) = (r, g, b, a);

    public Rgba(int r, int g, int b) : this((byte)r, (byte)g, (byte)b) { }

    public Rgba(byte r, byte g, byte b) => (R, G, B, A) = (r, g, b, byte.MaxValue);

    public byte R, G, B, A;

    public int Win32Value => R | (G << 8) | (B << 16) | (A << 24);

    public static implicit operator Rgba((int R, int G, int B) color) => new Rgba(color.R, color.G, color.B);

    public static implicit operator Rgba((byte R, byte G, byte B) color) => new Rgba(color.R, color.G, color.B);

    public static implicit operator Rgba((int R, int G, int B, int A) color) => new Rgba(color.R, color.G, color.B, color.A);

    public static implicit operator Rgba((byte R, byte G, byte B, byte A) color) => new Rgba(color.R, color.G, color.B, color.A);

    public static explicit operator Rgb(Rgba self) => new Rgb(self.R, self.G, self.B);
}