global using static Mossarium.Alpha.UI.OpenGL.Bins.Internal.Units;

//                                from      bit   byte  pixel long  slot
global using BitUnit = uint;   // to bit    ----  << 3  << 5  << 6  << 7
global using ByteUnit = uint;  // to byte   >> 3  ----  << 2  << 3  << 4
global using PixelUnit = uint; // to pixel  >> 5  >> 2  ----  << 1  << 2
global using LongUnit = uint;  // to long   >> 6  >> 3  >> 1  ----  << 1
global using SlotUnit = uint;  // to slot   >> 7  >> 4  >> 2  >> 1  ----
using System.Runtime.CompilerServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins.Internal;

static class Units
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint FitTo32(uint val) => (val + ((1 << 5) - 1)) >> 5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ByteUnit BitToByte(BitUnit val) => val >> 3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ByteUnit BitToByteFit(BitUnit val) => (val + ((1 << 3) - 1)) >> 3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PixelUnit BitToPixel(BitUnit val) => val >> 5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PixelUnit BitToPixelFit(BitUnit val) => (val + ((1 << 5) - 1)) >> 5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongUnit BitToLong(BitUnit val) => val >> 6;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongUnit BitToLongFit(BitUnit val) => (val + ((1 << 6) - 1)) >> 6;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SlotUnit BitToSlot(BitUnit val) => val >> 7;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SlotUnit BitToSlotFit(BitUnit val) => (val + ((1 << 7) - 1)) >> 7;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BitUnit ByteToBit(ByteUnit val) => val << 3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PixelUnit ByteToPixel(ByteUnit val) => val >> 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PixelUnit ByteToPixelFit(ByteUnit val) => (val + ((1 << 2) - 1)) >> 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongUnit ByteToLong(ByteUnit val) => val >> 3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongUnit ByteToLongFit(ByteUnit val) => (val + ((1 << 3) - 1)) >> 3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SlotUnit ByteToSlot(ByteUnit val) => val >> 4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SlotUnit ByteToSlotFit(ByteUnit val) => (val + ((1 << 4) - 1)) >> 4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BitUnit PixelToBit(PixelUnit val) => val << 5;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ByteUnit PixelToByte(PixelUnit val) => val << 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongUnit PixelToLong(PixelUnit val) => val >> 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongUnit PixelToLongFit(PixelUnit val) => (val + ((1 << 1) - 1)) >> 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SlotUnit PixelToSlot(PixelUnit val) => val >> 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SlotUnit PixelToSlotFit(PixelUnit val) => (val + ((1 << 2) - 1)) >> 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BitUnit LongToBit(LongUnit val) => val << 6;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ByteUnit LongToByte(LongUnit val) => val << 3;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PixelUnit LongToPixel(LongUnit val) => val << 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SlotUnit LongToSlot(LongUnit val) => val >> 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SlotUnit LongToSlotFit(LongUnit val) => (val + ((1 << 1) - 1)) >> 1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BitUnit SlotToBit(SlotUnit val) => val << 7;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ByteUnit SlotToByte(SlotUnit val) => val << 4;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PixelUnit SlotToPixel(SlotUnit val) => val << 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LongUnit SlotToLong(SlotUnit val) => val << 1;
}