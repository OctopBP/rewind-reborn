using System;

namespace Rewind.ECSCore.Enums {
	public enum GearTypeAState { Closed, Opening, Closing, Opened }

	public static class GearTypeAStateExt {
		public static bool isClosed(this GearTypeAState self) => self == GearTypeAState.Closed;
		public static bool isOpening(this GearTypeAState self) => self == GearTypeAState.Opening;
		public static bool isClosing(this GearTypeAState self) => self == GearTypeAState.Closing;
		public static bool isOpened(this GearTypeAState self) => self == GearTypeAState.Opened;

		public static int speedMultiplier(this GearTypeAState self) => self switch {
			GearTypeAState.Opening => 1,
			GearTypeAState.Closing => -1,
			GearTypeAState.Closed => 0,
			GearTypeAState.Opened => 0,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static GearTypeAState rewindState(this GearTypeAState self) => self switch {
			GearTypeAState.Opening => GearTypeAState.Closing,
			GearTypeAState.Closing => GearTypeAState.Opening,
			GearTypeAState.Closed => GearTypeAState.Opening,
			GearTypeAState.Opened => GearTypeAState.Closing,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
