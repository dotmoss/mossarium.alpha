using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe struct BlockList<TElement> : IDisposable
    where TElement : unmanaged
{
    public BlockList(uint initialCapacity)
    {
        blockInfos = new DynamicArray<BlockInfo>(initialCapacity);
        AddBlock();
    }

    public BlockList() : this(4) { }

    DynamicArray<BlockInfo> blockInfos;
    uint freeBlockIndex;

    public TElement* FirstElement => blockInfos[0]->Block->Elements;

    public TElement* this[uint index]
    {
        get
        {
            var blockIndex = index >> Block.ElementCountShift;
            var block = blockInfos[blockIndex]->Block;

            var slotLocalIndex = index & (Block.ElementCount - 1);
            var slot = block->Elements + slotLocalIndex;
            return slot;
        }
    }

    public uint AddElement(TElement slot)
    {
        var freeBlockIndex = this.freeBlockIndex;
        var block = blockInfos[freeBlockIndex];
        if (!block->HasFreeSlot)
        {
            freeBlockIndex = DetermineNextFreeBlock();
            block = blockInfos[freeBlockIndex];
        }

        var localIndex = block->AddElement(slot);
        var globalIndex = (freeBlockIndex << Block.ElementCountShift) | localIndex;
        return globalIndex;
    }

    public void RemoveElement(uint globalIndex)
    {
        var slotBlockIndex = globalIndex >> Block.ElementCountShift;
        var slotBlock = blockInfos[slotBlockIndex];

        var localIndex = globalIndex & Block.ElementCountMask;
        slotBlock->RemoveElement(localIndex);

        if (slotBlockIndex < freeBlockIndex)
        {
            freeBlockIndex = slotBlockIndex;
        }
    }

    uint DetermineNextFreeBlock()
    {
        for (var index = freeBlockIndex + 1; index < blockInfos.Count; index++)
        {
            var block = blockInfos[index];
            if (block->HasFreeSlot)
            {
                freeBlockIndex = index;
                return index;
            }
        }

        AddBlock();
        freeBlockIndex = blockInfos.Count - 1;
        return freeBlockIndex;
    }

    void AddBlock()
    {
        blockInfos.Increment();
        var block = blockInfos.Last;
        *block = new BlockInfo();
    }

    public void Dispose()
    {
        var array = blockInfos.Array;
        var count = blockInfos.Count;

        for (var i = 0; i < count; count++)
        {
            var block = array[i];
            block.Dispose();
        }

        blockInfos.Dispose();
    }

    public struct Block : IDisposable
    {
        public Block() => throw null!;

        fixed ulong elementPointer[64];

        Block* self => (Block*)Unsafe.AsPointer(ref this);

        public TElement* Elements => (TElement*)self->elementPointer;

        public void Dispose()
        {
            NativeMemory.Free(self);
        }

        public static Block* Allocate()
        {
            var block = (Block*)NativeMemory.Alloc((nuint)sizeof(Block));
            return block;
        }

        public const int ElementCount = 64;
        public const int ElementCountShift = 6;
        public const int ElementCountMask = ElementCount - 1;
    }

    public struct BlockInfo : IDisposable
    {
        public BlockInfo(Block* block)
        {
            Mask = 0;
            Block = block;
        }

        public ulong Mask { get; private set; }
        public Block* Block { get; private set; }

        public uint GetFreeElementIndex()
        {
#if DEBUG
            if (!HasFreeSlot)
                throw null!;
#endif

            var index = (uint)BitOperations.TrailingZeroCount(~Mask);
            return index;
        }

        public bool HasFreeSlot => Mask != 0xFFFFFFFFFFFFFFFFUL;

        public TElement* this[uint index] => Block->Elements + index;

        public uint AddElement(TElement element)
        {
            var index = GetFreeElementIndex();
            Block->Elements[index] = element;
            Mask |= 1UL << (int)index;

            return index;
        }

        public void RemoveElement(uint index)
        {
            Mask &= 1UL << (int)index;
        }

        public void Dispose()
        {
            Block->Dispose();
        }
    }
}