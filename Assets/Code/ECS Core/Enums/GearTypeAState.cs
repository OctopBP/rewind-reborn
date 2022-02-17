using System;

namespace Rewind.ECSCore.Enums {
	public enum GearTypeAState { Closed, Opening, Closing, Open }

	public static class GearTypeAStateExt {
		public static bool isClosed(this GearTypeAState self) => self == GearTypeAState.Closed;
		public static bool isOpening(this GearTypeAState self) => self == GearTypeAState.Opening;
		public static bool isClosing(this GearTypeAState self) => self == GearTypeAState.Closing;
		public static bool isOpen(this GearTypeAState self) => self == GearTypeAState.Open;

		public static int speedMultiplier(this GearTypeAState self) => self switch {
			GearTypeAState.Opening => 1,
			GearTypeAState.Closing => -1,
			GearTypeAState.Closed => 0,
			GearTypeAState.Open => 0,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}