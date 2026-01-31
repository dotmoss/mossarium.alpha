using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct BinTexture
{
    public ushort Width { get; internal set; }
    public ushort Height { get; internal set; }
    public uint Offset { get; internal set; }
    public ushort AllocationIndex { get; internal set; }

    public uint PixelCount => (uint)Width * Height;
    public uint ByteCount => PixelCount << 2;
    public uint SlotCount => (ByteCount + ((1 << 7) - 1)) >> 7;

    public uint SlotIndex => Offset >> 7;
}