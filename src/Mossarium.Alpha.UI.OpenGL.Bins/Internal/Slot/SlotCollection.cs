namespace Mossarium.Alpha.UI.OpenGL.Bins;

public unsafe struct SlotCollection : IDisposable
{
    public SlotCollection(uint totalLength)
    {
        this.totalLength = totalLength;
        slots = new BlockList<Slot>();
        InitializeBarriers();
    }

    BlockList<Slot> slots;

    Slot* leftBarrierSlot;
    uint barriersCount;
    uint totalLength;
    uint totalOccupied;
    Slot* currentSlot;

    void InitializeBarriers()
    {
        var lengthLimit = (uint)Slot.LengthLimit;
        var partsCount = (totalLength + lengthLimit - 1) / lengthLimit;
        barriersCount = partsCount + 1;

        var position = 0U;
        var nextIndex = partsCount + 1;
        AddSlot(new Slot(0, 0, 0, nextIndex));
        for (var i = 1U; i < partsCount; i++, position += lengthLimit)
            AddSlot(new Slot(position, 0, nextIndex, ++nextIndex));
        AddSlot(new Slot(position, 0, nextIndex, nextIndex));

        position = 0U;
        nextIndex = 0U;
        for (var i = 0U; i < partsCount; i++, position += lengthLimit)
            AddSlot(new Slot(position, lengthLimit, nextIndex, ++nextIndex));

        leftBarrierSlot = slots.FirstElement;
    }

    uint AddSlot(Slot slot)
    {
        var globalIndex = slots.AddElement(slot);
        return globalIndex;
    }

    void RemoveSlot(Slot slot)
    {
        var index = slots[slot.PrevSlotIndex]->NextSlotIndex;
        RemoveSlot(index);
    }

    void RemoveSlot(uint index)
    {
        slots.RemoveElement(index);
    }

    public Slot GetSuitableSlot(uint length)
    {
        var slot = *leftBarrierSlot;

        while (!slot.IsLast)
        {
            if (slot.Length >= length)
                return slot;

            slot = *slots[slot.NextSlotIndex];
        }

        return default;
    }

    public void NotifySpaceOccupied(Slot slot, uint position, uint length)
    {
        totalOccupied += length;

        var newPosition = position + length;
        var newLength = slot.Length - length;
        if (newLength == 0)
        {
            var prevSlotIndex = slot.PrevSlotIndex;
            var nextSlotIndex = slot.NextSlotIndex;
            var prevSlot = slots[prevSlotIndex];
            var nextSlot = slots[nextSlotIndex];
            
            var curIndex = prevSlot->NextSlotIndex;
            RemoveSlot(curIndex);

            prevSlot->SetNextSlot(nextSlotIndex);
            nextSlot->SetPrevSlot(prevSlotIndex);
        }
        else
        {
            slot.SetPosLen(newPosition, newLength);
        }
    }

    public void NotifySpaceReleased(uint position, uint length)
    {
        totalOccupied -= length;

        var leftBarrierIndex = position >> Slot.LengthBits;
        var leftBarrierSlot = slots[leftBarrierIndex];

        var rightBarrierIndex = leftBarrierIndex + 1;
        var rightBarrierSlot = slots[rightBarrierIndex];

        var leftSlotIndex = leftBarrierSlot->NextSlotIndex;
        var leftSlot = slots[leftSlotIndex];
        var rightSlotIndex = leftSlot->NextSlotIndex;
        var rightSlot = slots[rightSlotIndex];

        while (rightSlot->Position > position)
        {
            leftSlotIndex = rightSlotIndex;
            leftSlot = rightSlot;
            rightSlotIndex = leftSlot->NextSlotIndex;
            rightSlot = slots[rightSlotIndex];
        }

        if (!leftSlot->IsBarrier &&
            leftSlot->Position + leftSlot->Length == position && 
            !rightSlot->IsBarrier &&
            position + length == rightSlot->Position)
        {
            var newLength = leftSlot->Length + length + rightSlot->Length;
            var nextRightSlotIndex = rightSlot->NextSlotIndex;
            leftSlot->SetLenNextSlot(newLength, nextRightSlotIndex);
            RemoveSlot(rightSlotIndex);
        }
        else if (
            !leftSlot->IsBarrier &&
            leftSlot->Position + leftSlot->Length == position)
        {
            var newLength = leftSlot->Length + length;
            leftSlot->SetLen(newLength);
        }
        else if (
            !rightSlot->IsBarrier &&
            position + length == rightSlot->Position)
        {
            var newPosition = rightSlot->Position - length;
            var newLength = rightSlot->Length + length;
            rightSlot->SetPosLen(newPosition, newLength);
        }
        else
        {
            var slot = new Slot(position, length, leftSlotIndex, rightSlotIndex);
            var slotIndex = AddSlot(slot);
            leftSlot->SetNextSlot(slotIndex);
            rightSlot->SetPrevSlot(slotIndex);
        }
    }

    public void Dispose()
    {
        slots.Dispose();
    }
}