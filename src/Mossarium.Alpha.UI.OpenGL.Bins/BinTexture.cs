namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe struct BinTexture
{
    public ushort Width { get; internal set; }
    public ushort Height { get; internal set; }
    public uint PixelOffset { get; internal set; }

    public void Write(void* source)
    {
        var offset = (int)PixelToByte(PixelOffset);
        var length = (int)PixelToByte((PixelUnit)Width * Height);
        Atlas.buffer.Write(source, offset, length);
    }

    public void Delete() => Atlas.DeallocateTexture(this);

    public static BinTexture Create(PixelUnit width, PixelUnit height) => Atlas.AllocateTexture(width, height);
}