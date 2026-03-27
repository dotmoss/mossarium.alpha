namespace Mossarium.Alpha.UI.OpenGL.Bins;

public struct StaticTextures
{
    public StaticTextures() { }

    internal uint currentBufferPosition = 0;

    public BinTexture CreateTexture(PixelUnit width, PixelUnit height)
    {
        var pixelOffset = ByteToPixel(currentBufferPosition);
        currentBufferPosition += PixelToByte(width * height);

        var texture = new BinTexture
        {
            PixelOffset = pixelOffset,
            Width = (ushort)width,
            Height = (ushort)height
        };

        return texture;
    }
}