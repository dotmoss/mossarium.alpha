using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Mossarium.Alpha.UI.OpenGL.Bins.Internal;

internal unsafe struct BufferSpaceView : IDisposable
{
    public BufferSpaceView(uint bufferLength)
    {
        var bytesCount = (bufferLength + ((1 << 11) - 1)) >> 11 << 5;
        view = (ulong*)NativeMemory.AlignedAlloc(bytesCount, 32);
        Unsafe.InitBlock(view, 0, bytesCount);
    }

    public ulong* view;

    public unsafe uint GetSuitableIndex(uint startFromIndex, uint totalSlotCount, uint requestedSlotCount)
    {
        var totalBits = totalSlotCount << 6;
        var current = startFromIndex;
        var checkedBits = 0U;

        var ymm0 = Vector256<ulong>.AllBitsSet;

    RestartSearch:
        while (current < totalBits && checkedBits < totalBits)
        {
            var index = current >> 6;
            var offset = (int)(current & 63);
            if (offset == 0 && (index & 3) == 0)
            {
                while (current + 256 <= totalBits && checkedBits + 256 <= totalBits)
                {
                    var ymm1 = Avx.LoadAlignedVector256(view + (current >> 6));
                    if (Avx2.MoveMask(Avx2.CompareEqual(ymm1, ymm0).AsByte()) != -1)
                        break;

                    current += 256;
                    checkedBits += 256;
                }

                if (current >= totalBits)
                    goto HandleWrap;

                index = current >> 6;
            }

            var currentValue = ~(view[index] >> (int)(current & 63));
            if (currentValue == 0)
            {
                var skip = 64 - (current & 63U);
                current += skip;
                checkedBits += skip;
                if (current >= totalBits)
                    goto HandleWrap;

                continue;
            }

            var zeros = (uint)BitOperations.TrailingZeroCount(currentValue);
            current += zeros;
            checkedBits += zeros;
            if (current >= totalBits)
                goto HandleWrap;

            var found = 0U;
            while (found < requestedSlotCount)
            {
                var absolutePosition = current + found;
                if (absolutePosition >= totalBits)
                {
                    checkedBits += (totalBits - current);
                    current = 0;
                    goto RestartSearch;
                }

                var value = view[absolutePosition >> 6] >> (int)(absolutePosition & 63);
                if (value == 0)
                {
                    found += 64 - (absolutePosition & 63);
                }
                else
                {
                    found += (uint)BitOperations.TrailingZeroCount(value);
                    if (found >= requestedSlotCount)
                        return current;

                    var failSkip = found + 1;
                    current += failSkip;
                    checkedBits += failSkip;

                    if (current >= totalBits)
                        goto HandleWrap;

                    goto NextIteration;
                }
            }

            if (found >= requestedSlotCount)
                return current;

            NextIteration:;
        }

    HandleWrap:
        if (checkedBits < totalBits)
        {
            current = 0;
            goto RestartSearch;
        }

        return uint.MaxValue;
    }

    public unsafe void NotifyOccupied(uint slotPosition, uint slotCount)
    {
        var startIndex = slotPosition >> 6;
        var positionLength = slotPosition + slotCount - 1;
        var endIndex = positionLength >> 6;

        var startMask = ~Bmi2.X64.ZeroHighBits(ulong.MaxValue, slotPosition & 63);
        var endMask = Bmi2.X64.ZeroHighBits(ulong.MaxValue, (positionLength & 63) + 1);

        if (startIndex == endIndex)
        {
            view[startIndex] |= startMask & endMask;
            return;
        }

        view[startIndex] |= startMask;
        view[endIndex] |= endMask;

        var current = startIndex + 1;

        while (current < endIndex && (current & 3) != 0)
            view[current++] = ulong.MaxValue;

        var ymm0 = Vector256<ulong>.AllBitsSet;
        while (current + 4 <= endIndex)
        {
            ymm0.Store(view + current);
            current += 4;
        }

        while (current < endIndex)
            view[current++] = ulong.MaxValue;
    }

    public unsafe void NotifyReleased(uint slotPosition, uint slotCount)
    {
        var startIndex = slotPosition >> 6;
        var positionLength = slotPosition + slotCount - 1;
        var endIndex = positionLength >> 6;

        var startMask = ~Bmi2.X64.ZeroHighBits(ulong.MaxValue, slotPosition & 63);
        var endMask = Bmi2.X64.ZeroHighBits(ulong.MaxValue, (positionLength & 63) + 1);

        if (startIndex == endIndex)
        {
            view[startIndex] &= ~(startMask & endMask);
            return;
        }

        view[startIndex] &= ~startMask;
        view[endIndex] &= ~endMask;

        var current = startIndex + 1;

        while (current < endIndex && (current & 3) != 0)
            view[current++] = 0;

        var ymm0 = Vector256<ulong>.Zero;
        while (current + 4 <= endIndex)
        {
            ymm0.Store(view + current);
            current += 4;
        }

        while (current < endIndex)
            view[current++] = 0;
    }

    public void Dispose()
    {
        NativeMemory.AlignedFree(view);
    }
}