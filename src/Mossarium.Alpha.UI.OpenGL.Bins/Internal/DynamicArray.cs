using System.Runtime.InteropServices;

namespace Mossarium.Alpha.UI.OpenGL.Bins.Internal;

internal unsafe struct DynamicArray<T> : IDisposable
{
    public DynamicArray() : this(4) { }

    public DynamicArray(uint initialCapacity)
    {
        Capacity = initialCapacity;
        Array = (T*)NativeMemory.Alloc(Capacity << 3);
    }

    public T* Array { get; private set; }
    public uint Count { get; private set; }
    public uint Capacity { get; private set; }

    public T* First => Array;
    public T* Last => Array + (Count - 1);

    public T* this[uint index]
    {
        get => Array + index;
    }

    public void Increment()
    {
        Count++;

        if (Count == Capacity)
        {
            Expand();
        }
    }

    public void Decrement()
    {
        Count--;
    }

    void Expand()
    {
        Capacity <<= 1;
        Array = (T*)NativeMemory.Realloc(Array, Capacity << 3);
    }

    public void Dispose()
    {
        NativeMemory.Free(Array);
    }
}