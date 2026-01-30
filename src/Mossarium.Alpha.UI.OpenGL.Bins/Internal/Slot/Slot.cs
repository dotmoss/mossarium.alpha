namespace Mossarium.Alpha.UI.OpenGL.Bins;

public struct Slot
{
    public Slot(ulong position, ulong length, ulong prevSlotIndex, ulong nextSlotIndex)
    {
#if DEBUG
        if (position >= PositionLimit)
            throw null!;

        if (length >= LengthLimit)
            throw null!;

        if (prevSlotIndex >= SlotGlobalIndexLimit)
            throw null!;

        if (nextSlotIndex >= SlotGlobalIndexLimit)
            throw null!;
#endif

        SetData(position, length, prevSlotIndex, nextSlotIndex);
    }

    ulong data;

    public uint Position => (uint)(data & StartPositionMask);
    public uint Length => (uint)((data >> LengthShift) & StartLengthMask);
    public uint PrevSlotIndex => (uint)((data >> PrevSlotShift) & StartPrevSlotMask);
    public uint NextSlotIndex => (uint)(data >> NextSlotShift);
    public bool IsValid => data != 0UL;
    public bool IsLast => PrevSlotIndex == NextSlotIndex;
    public bool IsBarrier => Length == 0;

    public void SetData(ulong position, ulong length, ulong previousSlotIndex, ulong nextSlotIndex)
    {
        data = (position << PositionShift) | (length << LengthShift) | (previousSlotIndex << PrevSlotShift) | (nextSlotIndex << NextSlotShift);
    }

    public void SetPos(ulong position)
    {
        data = data & (/*PositionMask |*/ LengthMask | PrevSlotMask | NextSlotMask) | (position << PositionShift);
    }

    public void SetLen(ulong length)
    {
        data = data & (PositionMask /*| LengthMask*/ | PrevSlotMask | NextSlotMask) | (length << LengthShift);
    }

    public void SetPosLen(ulong position, ulong length)
    {
        data = data & (/*PositionMask | LengthMask |*/ PrevSlotMask | NextSlotMask) | (position << PositionShift) | (length << LengthShift);
    }

    public void SetPrevSlot(ulong prevSlotIndex)
    {
        data = data & (PositionMask | LengthMask /*| PrevSlotMask*/ | NextSlotMask) | (prevSlotIndex << PrevSlotShift);
    }

    public void SetNextSlot(ulong nextSlotIndex)
    {
        data = data & (PositionMask | LengthMask | PrevSlotMask /*| NextSlotMask*/) | (nextSlotIndex << NextSlotShift);
    }

    public void SetLenNextSlot(ulong length, ulong nextSlotIndex)
    {
        data = data & (PositionMask /*| LengthMask*/ | PrevSlotMask /*| NextSlotMask*/) | (length << LengthShift) | (nextSlotIndex << NextSlotShift);
    }

    public const int PositionBits = 22;
    public const int LengthBits = 14;
    const int SlotBits = 14;
    const int PrevSlotBits = SlotBits;
    const int NextSlotBits = SlotBits;

    const int PositionShift = 0;
    const int LengthShift = PositionBits;
    const int PrevSlotShift = LengthShift + LengthBits;
    const int NextSlotShift = PrevSlotShift + PrevSlotBits;

    const ulong StartPositionMask = (1UL << PositionBits) - 1;
    const ulong StartLengthMask = (1UL << LengthBits) - 1;
    const ulong StartPrevSlotMask = (1UL << PrevSlotBits) - 1;
    const ulong StartNextSlotMask = (1UL << NextSlotBits) - 1;

    const ulong PositionMask = StartPositionMask;
    const ulong LengthMask = StartLengthMask << LengthShift;
    const ulong PrevSlotMask = StartPrevSlotMask << PrevSlotShift;
    const ulong NextSlotMask = StartNextSlotMask << NextSlotShift;

    public const long PositionLimit = 1 << PositionBits;
    public const long LengthLimit = 1 << LengthBits;
    public const long SlotGlobalIndexLimit = 1 << SlotBits;
}