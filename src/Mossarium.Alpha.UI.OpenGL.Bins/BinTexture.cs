using Mossarium.Alpha.UI.OpenGL.Bins.Internal;
using System.Runtime.CompilerServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe struct BinTexture
{
    public ushort Width { get; internal set; }
    public ushort Height { get; internal set; }
    public uint PixelOffset { get; internal set; }

    public void Write(void* source)
    {
        var offset = (int)PixelToByte(PixelOffset);
        var length = (int)PixelToByte(PixelOffset);
        Atlas.buffer.Write(source, offset, length);
    }

    public void Delete()
    {
        var position = PixelToSlot(PixelOffset);
        var size = PixelToSlotFit((PixelUnit)Width * Height);
        BufferSpaceView.NotifyReleased(position, size);

        BinTextureCollection.RemoveTexture((BinTexture*)Unsafe.AsPointer(ref this));
    }

    public static BinTexture* Create(PixelUnit width, PixelUnit height)
    {
        var bytes = PixelToByte(width * height);
        var slots = ByteToSlotFit(bytes);

        var texture = Atlas.AllocateTexture(slots);
        texture->Width = (ushort)width;
        texture->Height = (ushort)height;
        return texture;
    }
}