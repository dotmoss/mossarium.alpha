using Mossarium.Alpha.UI.OpenGL.Bins.Contracts;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe partial struct Atlas<TBuffer>
    where TBuffer : unmanaged, IGlBufferContract
{
    public BinTexture* Allocate(ushort width, ushort height)
    {
        var byteCount = (uint)(width * height) << 2;
        var slotCount = byteCount + ((1 << 7) - 1) >> 7;

        var texture = textureCollection.Allocate(slotCount);
        if (texture is null)
        {
            DoubleGrowBuffer();
            return Allocate(width, height);
        }

        texture->Width = width;
        texture->Height = height;
        return texture;
    }

    public void Write(BinTexture* texture, void* source, uint length)
    {
        var offset = texture->Offset << 7;
        gpuBuffer.Write(source, offset, length);
    }
        
    public void Free(BinTexture* texture)
    {
        textureCollection.Deallocate(texture);
    }
}