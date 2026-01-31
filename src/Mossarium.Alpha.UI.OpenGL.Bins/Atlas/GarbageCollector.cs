using Mossarium.Alpha.UI.OpenGL.Bins.Contracts;
using Mossarium.Alpha.UI.OpenGL.Bins.Internal;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe partial struct Atlas<TBuffer>
    where TBuffer : unmanaged, IGlBufferContract
{
    void DoubleGrowBuffer()
    {
        GarbageCollect(gpuBufferLength, gpuBufferLength <<= 1);
    }

    void GarbageCollect()
    {
        GarbageCollect(gpuBufferLength, gpuBufferLength);
    }

    void GarbageCollect(uint oldBufferLength, uint newBufferLength)
    {
        var oldBufferData = NativeMemory.AlignedAlloc(oldBufferLength, 4096);
        gpuBuffer.Read(oldBufferData, 0, oldBufferLength);

        gpuBuffer.Allocate(newBufferLength);

        var newTextureCollection = new BinTextureCollection(textures, newBufferLength);
        var blocks = textures.Blocks;
        for (uint blockIndex = 0, textureIndex = 0; blockIndex < textures.BlockCount; blockIndex++)
        {
            var block = blocks[blockIndex];
            var mask = block->Mask;
            var elements = block->Block->Elements;
            for (var textureLocalIndex = 0; textureLocalIndex < BlockList<BinTexture>.Block.ElementCount; textureLocalIndex++)
            {
                var hasTexture = (mask & (1UL << textureLocalIndex)) == 0;
                if (!hasTexture)
                    continue;

                var oldTexture = elements + textureLocalIndex;
                var oldTextureOffset = oldTexture->Offset;
                var oldTextureLength = oldTexture->ByteCount;

                var newTexture = newTextureCollection.Allocate(oldTexture->SlotCount);
                newTexture->Width = oldTexture->Width;
                newTexture->Height = oldTexture->Height;

                Write(newTexture, (byte*)oldBufferData + oldTextureOffset, oldTextureLength);

                textureIndex++;
            }            
        }

        textureCollection = newTextureCollection;
    }
}