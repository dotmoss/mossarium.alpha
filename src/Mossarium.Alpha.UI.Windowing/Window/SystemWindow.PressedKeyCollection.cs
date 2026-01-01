using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using WindowsOS;

namespace Mossarium.Alpha.UI.Windowing;

unsafe partial class SystemWindow
{
	struct PressedKeyCollection
	{
		Vector256<ulong> states;
		uint modifiers;

		public bool IsPressed(Keys key) =>
			key switch
			{
				<= (Keys)0xFF => (*((ulong*)Unsafe.AsPointer(ref this) + ((uint)key >> 6)) & (1UL << ((int)key & LongBitsMask))) > 0,
				Keys.Shift => (modifiers & ShiftModifier) > 0,
				Keys.Control => (modifiers & ControlModifier) > 0,
				Keys.Alt => (modifiers & AltModifier) > 0,
				_ => false
			};

		public void NotifyPress(Keys key)
		{
			switch (key)
			{
				case <= (Keys)0xFF:
					var pointer = (ulong*)Unsafe.AsPointer(ref this) + ((uint)key >> 6);
					*pointer = *pointer | 1UL << ((int)key & LongBitsMask);
					break;
				case Keys.Shift:
					modifiers |= ShiftModifier;
					break;
				case Keys.Control:
					modifiers |= ControlModifier;
					break;
				case Keys.Alt:
					modifiers |= AltModifier;
					break;
			}
		}

		public void NotifyUnpress(Keys key)
		{
			switch (key)
			{
				case <= (Keys)0xFF:
					var pointer = (ulong*)Unsafe.AsPointer(ref this) + ((uint)key >> 6);
					*pointer = *pointer & ~(1UL << ((int)key & LongBitsMask));
					break;
				case Keys.Shift:
					modifiers &= ~ShiftModifier;
					break;
				case Keys.Control:
					modifiers &= ~ControlModifier;
					break;
				case Keys.Alt:
					modifiers &= ~AltModifier;
					break;
			}
		}

		public void Clear() => this = new PressedKeyCollection();
		
		public uint WriteOutPressedKeys(Keys* buffer)
		{
			var count = 0U;

			var modifiers = this.modifiers;
			if (modifiers != 0)
			{
                if ((modifiers & ShiftModifier) > 0)
                    buffer[count++] = Keys.Shift;

                if ((modifiers & ControlModifier) > 0)
                    buffer[count++] = Keys.Control;

                if ((modifiers & AltModifier) > 0)
                    buffer[count++] = Keys.Alt;
            }

			var ymm0 = states;
			if (ymm0 != Vector256<ulong>.Zero)
			{
                var mm64 = ymm0.GetElement(0);
				if (mm64 != 0)
					count = HandlePart64(0, mm64, buffer, count);

                mm64 = ymm0.GetElement(1);
                if (mm64 != 0)
                    count = HandlePart64(64, mm64, buffer, count);

                mm64 = ymm0.GetElement(2);
                if (mm64 != 0)
                    count = HandlePart64(128, mm64, buffer, count);

                mm64 = ymm0.GetElement(3);
                if (mm64 != 0)
                    count = HandlePart64(192, mm64, buffer, count);
            }

            return count;

			static uint HandlePart64(ulong valueBase, ulong value, Keys* buffer, uint count)
			{
				var lowerValue = value & 0xFFFFFFFFUL;
				if (lowerValue != 0)
					count = HandlePart32(valueBase, lowerValue, buffer, count);

                var upperValue = value >> 32;
                if (upperValue != 0)
                    count = HandlePart32(valueBase + 32, upperValue, buffer, count);

				return count;

                static uint HandlePart32(ulong valueBase, ulong value, Keys* buffer, uint count)
				{
                    var lowerValue = value & 0xFFFFUL;
                    if (lowerValue != 0)
                        count = HandlePart16(valueBase, lowerValue, buffer, count);

                    var upperValue = value >> 16;
                    if (upperValue != 0)
                        count = HandlePart16(valueBase + 16, upperValue, buffer, count);

					return count;

					static uint HandlePart16(ulong valueBase, ulong value, Keys* buffer, uint count)
					{
                        for (var index = 0U; index < 16; index++)
                            if ((value & (1UL << (int)index)) != 0)
                                buffer[count++] = (Keys)(valueBase + index);

                        return count;
					}
                }
            }
        }

		public static PressedKeyCollection Insance;

		const int LongBitsMask = (1 << 6) - 1;
		const uint ShiftModifier = 1 << 0;
		const uint ControlModifier = 1 << 1;
		const uint AltModifier = 1 << 2;
	}
}