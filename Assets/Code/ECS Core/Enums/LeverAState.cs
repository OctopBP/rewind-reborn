using System;

namespace Rewind.ECSCore.Enums {
	public enum LeverAState : short { Closed, Opened }

	public static class LeverAStateExt {
		public static bool isClosed(this LeverAState self) => self == LeverAState.Closed;
		public static bool isOpened(this LeverAState self) => self == LeverAState.Opened;
		
		public static LeverAState rewindState(this LeverAState self) => self switch {
			LeverAState.Closed => LeverAState.Opened,
			LeverAState.Opened => LeverAState.Closed,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
