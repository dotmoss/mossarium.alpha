using DebugProfiler;
using Mossarium.Alpha.UI.OpenGL.Bins.Internal;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe static partial class Atlas
{
    internal static GlTextureBuffer buffer;
    internal static uint bufferLength;

    public static void Initialize()
    {
        const uint GlobalAtlasTextureBindingIndex = 0;

        Profiler.Push(ProfileStage.AtlasInitialization);

        buffer = new GlTextureBuffer();
        bufferLength = PixelToByte(2048 * 2048);
        buffer.Allocate(bufferLength);

        var bufferTextureRgb8View = buffer.DefineTextureRgb8();
        bufferTextureRgb8View.Active(GlobalAtlasTextureBindingIndex);

        BufferSpaceView.Allocate(bufferLength);

        Profiler.Pop();
    }

    internal static BinTexture* AllocateTexture(SlotUnit slots)
    {
        var slotIndex = BufferSpaceView.GetSuitableIndex(slots);
        if (slotIndex == uint.MaxValue)
        {
            GarbageCollect(Atlas.bufferLength, Atlas.bufferLength <<= 1);
            return AllocateTexture(slots);
        }

        BufferSpaceView.NotifyOccupied(slotIndex, slots);

        var textureData = new BinTexture();
        textureData.PixelOffset = SlotToPixel(slotIndex);

        var texture = BinTextureCollection.AddTexture(textureData);
        return texture;
    }

    internal static void GarbageCollect(uint oldBufferLength, uint newBufferLength)
    {
        var oldBufferData = NativeMemory.AlignedAlloc(oldBufferLength, 4096);
        buffer.Read(oldBufferData, 0, (int)oldBufferLength);

        buffer.Allocate(newBufferLength);

        BufferSpaceView.Allocate(newBufferLength);
        var blocks = BinTextureCollection.blocks;
        for (uint blockIndex = 0, textureIndex = 0; blockIndex < BinTextureCollection.blockCount; blockIndex++)
        {
            var block = blocks + blockIndex;
            for (var textureLocalIndex = 0U; textureLocalIndex < 64; textureLocalIndex++)
            {
                if (!block->HasTextureAt(textureLocalIndex))
                    continue;

                var oldTexture = block->Get(textureLocalIndex);
                var textureSize = (PixelUnit)oldTexture->Width * oldTexture->Height;

                var newTexture = AllocateTexture(PixelToSlotFit(textureSize));
                newTexture->Width = oldTexture->Width;
                newTexture->Height = oldTexture->Height;

                var offset = PixelToByte(oldTexture->PixelOffset);
                var source = (byte*)oldBufferData + offset;
                newTexture->Write(source);

                textureIndex++;
            }
        }
    }

    static Atlas()
    {
        Profiler.Register<ProfileStage>("OpenGL.Bins");
    }
}