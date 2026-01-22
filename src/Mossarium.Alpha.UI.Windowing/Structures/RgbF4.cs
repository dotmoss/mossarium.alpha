using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.Windowing.Structures;

[StructLayout(LayoutKind.Sequential, Size = 12)]
public struct RgbF4
{
    public RgbF4(float r, float g, float b) => (R, G, B) = (r, g, b);

    public float R, G, B;

    public static implicit operator RgbF4((float R, float G, float B) color) => new RgbF4(color.R, color.G, color.B);
}