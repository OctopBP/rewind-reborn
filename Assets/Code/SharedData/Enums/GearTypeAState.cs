using System;

namespace Rewind.SharedData {
	public enum GearTypeAState : short { Closed, Opening, Closing, Opened }

	public static class GearTypeAStateExt {
		public static bool isClosed(this GearTypeAState self) => self == GearTypeAState.Closed;
		public static bool isOpening(this GearTypeAState self) => self == GearTypeAState.Opening;
		public static bool isClosing(this GearTypeAState self) => self == GearTypeAState.Closing;
		public static bool isOpened(this GearTypeAState self) => self == GearTypeAState.Opened;
		public static bool isClosedOrOpened(this GearTypeAState self) =>
			self is GearTypeAState.Closed or GearTypeAState.Opened;

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
			GearTypeAState.Closed => GearTypeAState.Closed,
			GearTypeAState.Opened => GearTypeAState.Opened,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static void fold(
			this GearTypeAState self, Action<GearTypeAState> onClosing,
			Action<GearTypeAState> onOpening, Action<GearTypeAState> onClosed, Action<GearTypeAState> onOpened
		) => (self switch {
			GearTypeAState.Opening => onOpening,
			GearTypeAState.Closing => onClosing,
			GearTypeAState.Closed => onClosed,
			GearTypeAState.Opened => onOpened,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		}).Invoke(self);
		
		public static T fold<T>(
			this GearTypeAState self, Func<GearTypeAState, T> onClosing,
			Func<GearTypeAState, T> onOpening, Func<GearTypeAState, T> onClosed, Func<GearTypeAState, T> onOpened
		) => (self switch {
			GearTypeAState.Opening => onOpening,
			GearTypeAState.Closing => onClosing,
			GearTypeAState.Closed => onClosed,
			GearTypeAState.Opened => onOpened,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		}).Invoke(self);
	}
}
