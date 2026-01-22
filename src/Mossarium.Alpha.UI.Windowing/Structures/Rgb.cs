using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.Windowing.Structures;

[StructLayout(LayoutKind.Sequential, Size = 3)]
public struct Rgb
{
    public Rgb(int r, int g, int b) : this((byte)r, (byte)g, (byte)b) { }

    public Rgb(byte r, byte g, byte b) => (R, G, B) = (r, g, b);

    public byte R, G, B;

    public int Win32Value => R | (G << 8) | (B << 16);

    public static implicit operator Rgb((int R, int G, int B) color) => new Rgb(color.R, color.G, color.B);

    public static implicit operator Rgb((byte R, byte G, byte B) color) => new Rgb(color.R, color.G, color.B);

    public static implicit operator Rgba(Rgb self) => new Rgba(self.R, self.G, self.B, (byte)255);
}