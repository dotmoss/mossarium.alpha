using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Mossarium.Alpha.UI.OpenGL.Bins.Internal;

internal unsafe static class GpuBufferSpaceView
{
    static byte* view;
    static BitUnit viewLengthBits;
    static BitUnit lastViewBit;

    public static void Allocate(uint bufferLength)
    {
        var viewLengthBits = ByteToSlot(bufferLength);
        var viewLengthBytes = FitTo32(BitToByte(viewLengthBits));
        view = (byte*)NativeMemory.AllocZeroed(viewLengthBytes);

        viewLengthBits = viewLengthBits - 512;
    }

    public static void Reallocate(uint bufferLength)
    {
        var bits = ByteToSlot(bufferLength);
        var bytes = FitTo32(BitToByte(bits));
        view = (byte*)NativeMemory.Realloc(view, bytes);

        var oldBytes = (viewLengthBits >> 6) + 64;
        Unsafe.InitBlock(view + oldBytes, 0, bytes - oldBytes);

        viewLengthBits = bits - 512;
        lastViewBit = 0;
    }

    public static uint GetSuitableSpace(uint requestedSlots)
    {
        var pointer = (ulong*)view;
        var totalBits = viewLengthBits - requestedSlots;
        var bitPosition = lastViewBit;

        var inLongBitPosition = bitPosition & 63;
        var bitSpace = inLongBitPosition + requestedSlots;
        var slotsToFound = requestedSlots;

        if (bitSpace < 128)
        {
            while (bitPosition < totalBits)
            {
                if (bitSpace <= 64)
                {
                    var longPosition = bitPosition >> 6;
                    var value = pointer[longPosition] >> (int)inLongBitPosition;
                    var zeros = BitOperations.TrailingZeroCount(value);
                    if (zeros >= slotsToFound)
                        goto Found;

                    var ones = BitOperations.TrailingZeroCount(~(value >> zeros));
                    if (ones == 0)
                    {
                        bitPosition += (uint)zeros;
                        slotsToFound -= (uint)zeros;
                    }
                    else
                    {
                        bitPosition += (uint)zeros + (uint)ones;
                        slotsToFound = requestedSlots;
                    }
                }
                else
                {
                    var longPosition = bitPosition >> 6;

                    var leftValue = pointer[longPosition];
                    var leftZeros = BitOperations.LeadingZeroCount(leftValue);

                    var rightValue = pointer[longPosition + 1];
                    var rightZeros = BitOperations.TrailingZeroCount(rightValue);

                    var zeros = leftZeros + rightZeros;
                    if (leftZeros + rightZeros >= slotsToFound)
                    {
                        //bitPosition += 64 - (uint)leftZeros;
                        goto Found;
                    }

                    var ones = BitOperations.TrailingZeroCount(~(rightValue >> rightZeros));
                    if (ones == 0)
                    {
                        slotsToFound -= (uint)zeros;
                        bitPosition = (longPosition + 2) << 6;
                    }
                    else
                    {
                        slotsToFound = requestedSlots;
                        bitPosition = ((longPosition + 1) << 6) + (uint)rightZeros + (uint)ones;
                    }
                }

                inLongBitPosition = bitPosition & 63;
                bitSpace = inLongBitPosition + slotsToFound;
            }
        }
        else
        {
            var longPosition = bitPosition >> 6;
            while (bitPosition < totalBits)
            {
                var leftZeros = BitOperations.LeadingZeroCount(pointer[longPosition]);
                if (leftZeros == 0)
                {
                    for (longPosition++; ; longPosition += 4)
                    {
                        var ymmValue = Avx.LoadVector256(pointer + longPosition);
                        var ymmMask = Avx2.CompareEqual(ymmValue, Vector256<ulong>.Zero);
                        var mm4mask = Avx.MoveMask(ymmMask.AsDouble());
                        if (mm4mask == 0b0000)
                            continue;

                        var elementIndex = BitOperations.TrailingZeroCount(mm4mask);
                        longPosition += (uint)elementIndex - 1;
                        goto Retry;
                    }
                }

                bitPosition += 64 - (uint)leftZeros;

                var bitEndPosition = bitPosition + requestedSlots - 1;
                var longEndPosition = bitEndPosition >> 6;

                var rightValue = pointer[longEndPosition];
                var rightZeros = BitOperations.TrailingZeroCount(rightValue);
                var requiredRightZeros = bitEndPosition & 63;
                if (rightZeros < requiredRightZeros)
                {
                    longPosition = longEndPosition;
                    goto Retry;
                }

                var longStartPosition = longPosition + 1;
                longPosition = longEndPosition;
                if (bitSpace > 320)
                {
                    var ymmLongStartPosition = longStartPosition + (uint)((longEndPosition - longStartPosition) & ~3);
                    do
                    {
                        longPosition -= 4;
                        var ymmValue = Avx.LoadVector256(pointer + longPosition);
                        var ymmMask = Avx2.CompareEqual(ymmValue, Vector256<ulong>.Zero);
                        var mm4mask = (uint)Avx.MoveMask(ymmMask.AsDouble());
                        if (mm4mask == 0b1111)
                            continue;

                        var elementIndex = 30 - BitOperations.LeadingZeroCount(mm4mask);
                        longPosition += (uint)elementIndex;
                        goto Retry;
                    }
                    while (longPosition > ymmLongStartPosition);
                }

                do
                {
                    if (pointer[--longPosition] != 0UL)
                        goto Retry;
                }
                while (longPosition > longStartPosition);

                goto Found;
            Retry:;
                bitPosition = longPosition << 6;
            }
        }

        return ~0U;

    Found:
        lastViewBit = bitPosition + requestedSlots;
        return bitPosition;
    }

    public static void OccupySpace(uint slotPosition, uint slotCount)
    {
        var pointer = (ulong*)view;

        var longPosition = slotPosition >> 6;
        var inLongBitPosition = slotPosition & 63;
        var bitSpace = inLongBitPosition + slotCount;
        if (bitSpace <= 64)
        {
            pointer[longPosition] |= Bmi2.X64.ZeroHighBits(~0UL, slotCount) << (int)inLongBitPosition;
            return;
        }

        pointer[longPosition] |= ~Bmi2.X64.ZeroHighBits(~0UL, inLongBitPosition);
        var slotEndPosition = slotPosition + slotCount - 1;
        var longEndPosition = slotEndPosition >> 6;

        longPosition++;
        if (bitSpace > 320)
        {
            var ymmEndPosition = longPosition + ((longEndPosition - longPosition) & ~3);
            for (; longPosition < ymmEndPosition; longPosition += 4)
                Avx.Store(pointer + longPosition, Vector256<ulong>.AllBitsSet);
        }

        for (; longPosition < longEndPosition; longPosition++)
            pointer[longPosition] = ~0UL;
        pointer[longEndPosition] |= Bmi2.X64.ZeroHighBits(~0UL, (slotEndPosition & 63) + 1);
    }

    public static void ReleaseSpace(uint slotPosition, uint slotCount)
    {
        var pointer = (ulong*)view;

        var longPosition = slotPosition >> 6;
        var inLongBitPosition = slotPosition & 63;
        var bitSpace = inLongBitPosition + slotCount;
        if (bitSpace <= 64)
        {
            pointer[longPosition] &= ~(Bmi2.X64.ZeroHighBits(~0UL, slotCount) << (int)inLongBitPosition);
            return;
        }

        pointer[longPosition] = Bmi2.X64.ZeroHighBits(pointer[longPosition], inLongBitPosition);
        var slotEndPosition = slotPosition + slotCount - 1;
        var longEndPosition = slotEndPosition >> 6;

        longPosition++;
        if (bitSpace > 320)
        {
            var ymmEndPosition = longPosition + ((longEndPosition - longPosition) & ~3);
            for (; longPosition < ymmEndPosition; longPosition += 4)
                Avx.Store(pointer + longPosition, Vector256<ulong>.Zero);
        }

        for (; longPosition < longEndPosition; longPosition++)
            pointer[longPosition] = 0UL;
        pointer[longEndPosition] &= ~Bmi2.X64.ZeroHighBits(~0UL, (slotEndPosition & 63) + 1);
    }
}