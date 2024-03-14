using System;

namespace Rewind.SharedData
{
	public enum ButtonAState : short { Closed, Opened }

	public static class ButtonAStateExt
	{
		public static bool IsClosed(this ButtonAState self) => self == ButtonAState.Closed;
		public static bool IsOpened(this ButtonAState self) => self == ButtonAState.Opened;
		
		public static ButtonAState RewindState(this ButtonAState self) => self switch
		{
			ButtonAState.Closed => ButtonAState.Opened,
			ButtonAState.Opened => ButtonAState.Closed,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
