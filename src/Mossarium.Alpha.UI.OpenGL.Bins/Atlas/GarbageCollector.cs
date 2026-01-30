using Mossarium.Alpha.UI.OpenGL.Bins.Contracts;
using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe partial struct Atlas<TBuffer>
    where TBuffer : unmanaged, IGlBufferContract
{
    void DoubleGrowBuffer()
    {
        GarbageCollect(gpuBufferLength, gpuBufferLength << 1);
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

        var oldSlots = slots;
        var newSlots = new SlotCollection();


    }
}