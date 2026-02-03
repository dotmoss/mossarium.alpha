using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Mossarium.Alpha.UI.OpenGL.Bins.Internal;

internal unsafe static class BufferSpaceView
{
    static byte* view;
    static BitUnit bits;
    static BitUnit lastBit;
    static bool firstPass;

    public static void Allocate(uint bufferLength)
    {
        var newBits = ByteToSlot(bufferLength);
        var bytes = FitTo32(BitToByte(newBits));
        if (newBits != bits)
        {
            bits = newBits;
            view = (byte*)NativeMemory.AlignedAlloc(bytes, 32);
        }

        Unsafe.InitBlock(view, 0, bytes);
        firstPass = true;
    }

    public static uint GetSuitableIndex(uint requestedSlots)
    {
        var pointer = (ulong*)view;
        var bits = BufferSpaceView.bits;
        var lastBit = BufferSpaceView.lastBit;
        
        if (firstPass)
        {
            if (requestedSlots <= bits - lastBit)
                return lastBit;

            firstPass = false;
        }

        var currentBit = lastBit;
        var ulongIndex = currentBit >> 6;
        int bitOffset = (int)(currentBit & 63);
        var bitBarrier = bits - requestedSlots - 256;

        while (currentBit <= bitBarrier)
        {
            if ((currentBit & 255) == 0)
            {
                while (currentBit <= bitBarrier)
                {
                    var ymm0 = Avx.LoadVector256(pointer + (currentBit >> 6));
                    if (Avx2.CompareEqual(ymm0, Vector256<ulong>.AllBitsSet) == Vector256<ulong>.AllBitsSet)
                    {
                        currentBit += 256;
                        continue;
                    }
                    break;
                }
            }

            var value = pointer[currentBit >> 6] >> (int)(currentBit & 63);
            var longRemaining = 64 - (int)(currentBit & 63);

            if ((value & 1) != 0)
            {
                var firstZero = (uint)Bmi1.X64.TrailingZeroCount(~value);
                currentBit += firstZero;
                continue;
            }

            var found = 0U;
            var checkIndex = currentBit;
            while (found < requestedSlots && checkIndex < bits)
            {
                var block = pointer[checkIndex >> 6] >> (int)(checkIndex & 63);
                var limit = 64 - (checkIndex & 63);

                var zeros = (uint)Bmi1.X64.TrailingZeroCount(block);
                var iterationFound = Math.Min(zeros, limit);

                found += iterationFound;
                checkIndex += iterationFound;

                if (iterationFound < limit) 
                    break;
            }

            if (found >= requestedSlots)
                return currentBit;

            currentBit = checkIndex + 1;
        }

        return 0xFFFFFFFF;
    }

    public static void NotifyOccupied(uint slotPosition, uint slotCount)
    {
        var pointer = (ulong*)view;
        var startIndex = slotPosition >> 6;
        var positionLength = slotPosition + slotCount - 1;
        var endIndex = positionLength >> 6;

        var startMask = ~Bmi2.X64.ZeroHighBits(ulong.MaxValue, slotPosition & 63);
        var endMask = Bmi2.X64.ZeroHighBits(ulong.MaxValue, (positionLength & 63) + 1);

        if (startIndex == endIndex)
        {
            pointer[startIndex] |= startMask & endMask;
            return;
        }

        pointer[startIndex] |= startMask;
        pointer[endIndex] |= endMask;

        var current = startIndex + 1;

        while (current < endIndex && (current & 3) != 0)
            pointer[current++] = ulong.MaxValue;

        var ymm0 = Vector256<ulong>.AllBitsSet;
        while (current + 4 <= endIndex)
        {
            ymm0.Store(pointer + current);
            current += 4;
        }

        while (current < endIndex)
            pointer[current++] = ulong.MaxValue;
    }

    public static void NotifyReleased(uint slotPosition, uint slotCount)
    {
        var pointer = (ulong*)view;
        var startIndex = slotPosition >> 6;
        var positionLength = slotPosition + slotCount - 1;
        var endIndex = positionLength >> 6;

        var startMask = ~Bmi2.X64.ZeroHighBits(ulong.MaxValue, slotPosition & 63);
        var endMask = Bmi2.X64.ZeroHighBits(ulong.MaxValue, (positionLength & 63) + 1);

        if (startIndex == endIndex)
        {
            pointer[startIndex] &= ~(startMask & endMask);
            return;
        }

        pointer[startIndex] &= ~startMask;
        pointer[endIndex] &= ~endMask;

        var current = startIndex + 1;

        while (current < endIndex && (current & 3) != 0)
            pointer[current++] = 0;

        var ymm0 = Vector256<ulong>.Zero;
        while (current + 4 <= endIndex)
        {
            ymm0.Store(pointer + current);
            current += 4;
        }

        while (current < endIndex)
            pointer[current++] = 0;
    }
}