using System;

namespace Rewind.ECSCore.Enums {
	public enum ButtonAState : short { Closed, Opened }

	public static class ButtonAStateExt {
		public static bool isClosed(this ButtonAState self) => self == ButtonAState.Closed;
		public static bool isOpened(this ButtonAState self) => self == ButtonAState.Opened;
		
		public static ButtonAState rewindState(this ButtonAState self) => self switch {
			ButtonAState.Closed => ButtonAState.Opened,
			ButtonAState.Opened => ButtonAState.Closed,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
