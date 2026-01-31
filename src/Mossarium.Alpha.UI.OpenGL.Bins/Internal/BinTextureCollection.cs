namespace Mossarium.Alpha.UI.OpenGL.Bins.Internal;

internal unsafe struct BinTextureCollection : IDisposable
{
    public BinTextureCollection(BlockList<BinTexture> textures, uint bufferLength)
    {
        this.textures = textures;
        spaceView = new BufferSpaceView(bufferLength);
        totalSlotCount = (bufferLength + ((1 << 7) - 1)) >> 7;
    }

    uint totalSlotCount;
    BlockList<BinTexture> textures;
    BufferSpaceView spaceView;
    uint nextApproximateSlot;

    public BinTexture* Allocate(uint slotCount)
    {
        var slotIndex = spaceView.GetSuitableIndex(nextApproximateSlot, totalSlotCount, slotCount);
        if (slotIndex == uint.MaxValue)
            return null;

        nextApproximateSlot = slotIndex + slotCount;

        spaceView.NotifyOccupied(slotIndex, slotCount);

        var textureData = new BinTexture();
        textureData.Offset = slotIndex << 7;

        var textureIndex = (ushort)textures.AddElement(textureData);
        var texture = textures[textureIndex];
        texture->AllocationIndex = textureIndex;

        return texture;
    }

    public void Deallocate(BinTexture* texture)
    {
        spaceView.NotifyReleased(texture->SlotIndex, texture->SlotCount);
        textures.RemoveElement(texture->AllocationIndex);
    }

    public void Dispose()
    {
        spaceView.Dispose();
    }
}