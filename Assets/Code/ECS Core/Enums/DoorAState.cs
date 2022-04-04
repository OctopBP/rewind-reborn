using System;

namespace Rewind.ECSCore.Enums {
	public enum DoorAState : short { Closed, Opened }

	public static class DoorAStateExt {
		public static bool isClosed(this DoorAState self) => self == DoorAState.Closed;
		public static bool isOpened(this DoorAState self) => self == DoorAState.Opened;
		
		public static DoorAState rewindState(this DoorAState self) => self switch {
			DoorAState.Closed => DoorAState.Opened,
			DoorAState.Opened => DoorAState.Closed,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
