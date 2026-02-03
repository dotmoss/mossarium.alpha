using System.Numerics;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins.Internal;

unsafe static class BinTextureCollection
{
    static BinTextureCollection()
    {
        blockCapacity = 16;
        blocks = (Block*)NativeMemory.Alloc((uint)sizeof(Block) * blockCapacity);

        InitializeNewBlock();
    }

    internal static Block* blocks;
    internal static ushort blockCount;
    internal static ushort blockCapacity;
    internal static ushort freeBlockIndex;

    static void InitializeNewBlock()
    {
        var blockIndex = blockCount++;
        var block = blocks + blockIndex;
        block->mask = 0xFFFFFFFFFFFFFFFFUL;
        block->textures = (BinTexture*)NativeMemory.AlignedAlloc((uint)sizeof(BinTexture) * 64 + sizeof(ushort), (uint)sizeof(BinTexture) * 64);
        *(ushort*)&block->textures[64] = blockIndex;
    }

    public static BinTexture* GetTexture(uint index)
    {
        var block = blocks + (index >> 6);
        var texture = block->Get(index & ((1 << 6) - 1));
        return texture;
    }

    public static BinTexture* AddTexture(BinTexture texture)
    {
        var freeBlockIndex = BinTextureCollection.freeBlockIndex;
        var block = blocks + freeBlockIndex;
        var freeTextureIndex = block->FreeIndex;
        if (freeTextureIndex == 64)
        {
            while (++freeBlockIndex < blockCount)
            {
                block = blocks + freeBlockIndex;
                freeTextureIndex = block->FreeIndex;
                if (freeTextureIndex == 64)
                    continue;

                BinTextureCollection.freeBlockIndex = freeBlockIndex;
                goto AddElement;
            }
            freeTextureIndex = 0;

            BinTextureCollection.freeBlockIndex = freeBlockIndex;
            if (freeBlockIndex == blockCapacity)
            {
                blockCapacity <<= 1;
                blocks = (Block*)NativeMemory.Realloc(blocks, (uint)sizeof(Block) * blockCapacity);
            }
            InitializeNewBlock();
        }

    AddElement:
        block->Set(freeTextureIndex, texture);
        return block->Get(freeTextureIndex);
    }

    public static void RemoveTexture(BinTexture* texture)
    {
        var blockSize = (uint)sizeof(BinTexture) * 64;
        var textureAddress = (nint)texture;
        var textureBaseAddress = textureAddress & ~blockSize;
        var textureIndex = (uint)(textureAddress & blockSize) >> 3;
        var blockIndex = *(ushort*)(textureBaseAddress + blockSize);
        RemoveTexture(textureIndex, blockIndex);
    }

    public static void RemoveTexture(uint index)
    {
        var blockIndex = index >> 6;
        var textureIndex = index & ((1 << 6) - 1);
        RemoveTexture(blockIndex, textureIndex);
    }

    public static void RemoveTexture(uint blockIndex, uint textureIndex)
    {
        var block = blocks + blockIndex;
        block->Remove(textureIndex);

        if (blockIndex < freeBlockIndex)
            freeBlockIndex = (ushort)blockIndex;
    }

    public struct Block
    {
        internal ulong mask;
        internal BinTexture* textures;

        public uint FreeIndex => (uint)BitOperations.TrailingZeroCount(mask);

        public bool HasTextureAt(uint index) => (mask & (1UL << (int)index)) == 0;

        public BinTexture* Get(uint index)
        {
            return textures + index;
        }

        public void Set(uint index, BinTexture texture)
        {
            textures[index] = texture;
            mask &= 1UL << (int)index;
        }

        public void Remove(uint index)
        {
            mask |= 1UL << (int)index;
        }
    }
}