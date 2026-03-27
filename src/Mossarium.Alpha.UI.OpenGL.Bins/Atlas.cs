using DebugProfiler;
using Mossarium.Alpha.UI.OpenGL.Bins.Internal;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe static partial class Atlas
{
    internal static GlTextureBuffer buffer;
    internal const uint staticBufferLength = 4096;
    internal static uint dynamicBufferLength;
    internal static uint totalBufferLength => staticBufferLength + dynamicBufferLength;

    public static void Initialize<TInitializer>()
        where TInitializer : IStaticTexturesInitializer
    {
        const uint GlobalAtlasTextureBindingIndex = 0;

        Profiler.Push(ProfileStage.AtlasInitialization);

        buffer = new GlTextureBuffer();
        Allocate(PixelToByte(2048 * 2048));

        buffer.DefineTextureRgb8().Active(GlobalAtlasTextureBindingIndex);

        InitializeStaticTextures<TInitializer>();

        Profiler.Pop();
    }

    static void InitializeStaticTextures<TInitializer>()
        where TInitializer : IStaticTexturesInitializer
    {
        Profiler.Push(ProfileStage.AtlasInitialization);

        var staticTextures = new StaticTextures();
        TInitializer.InitializeStaticTextures(&staticTextures);

        if (Profiler.IsEnabled)
        {
            var percentUsage = Math.Round((float)staticTextures.currentBufferPosition / staticBufferLength * 100, 2);
            Profiler.Log($"Static texture buffer usage: {staticTextures.currentBufferPosition} of {staticBufferLength} ({percentUsage}%)");
        }

        Profiler.Pop();
    }

    static void Allocate(uint length)
    {
        dynamicBufferLength = length;
        buffer.Allocate((int)totalBufferLength);

        GpuBufferSpaceView.Allocate(dynamicBufferLength);
    }

    static void Reallocate(uint length)
    {
        var oldBufferLength = totalBufferLength;
        dynamicBufferLength = length;

        var oldBufferData = NativeMemory.AlignedAlloc(oldBufferLength, 4096);
        buffer.Read(oldBufferData, 0, (int)oldBufferLength);
        buffer.Allocate((int)totalBufferLength);
        buffer.Write(oldBufferData, 0, (int)oldBufferLength);
        NativeMemory.AlignedFree(oldBufferData);

        GpuBufferSpaceView.Reallocate(dynamicBufferLength);
    }

    static void Expand()
    {
        Reallocate(dynamicBufferLength + PixelToByte(512 * 512));
    }

    internal static BinTexture AllocateTexture(PixelUnit width, PixelUnit height)
    {
        var bytes = PixelToByte(width * height);
        var slots = ByteToSlotFit(bytes);

        var slotIndex = GpuBufferSpaceView.GetSuitableSpace(slots);
        if (slotIndex == uint.MaxValue)
        {
            Expand();
            return AllocateTexture(width, height);
        }

        GpuBufferSpaceView.OccupySpace(slotIndex, slots);

        var texture = new BinTexture
        {
            PixelOffset = ByteToPixel(SlotToByte(slotIndex) + staticBufferLength),
            Width = (ushort)width,
            Height = (ushort)height
        };

        return texture;
    }

    internal static void DeallocateTexture(BinTexture texture)
    {
        var position = ByteToSlot(PixelToByte(texture.PixelOffset) - staticBufferLength);
        var size = PixelToSlotFit((PixelUnit)texture.Width * texture.Height);

        GpuBufferSpaceView.ReleaseSpace(position, size);
    }

    static Atlas()
    {
        Profiler.Register<ProfileStage>("OpenGL.Bins");
    }
}