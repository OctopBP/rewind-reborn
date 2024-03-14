using System;

namespace Rewind.SharedData
{
	public enum LeverAState : short { Closed, Opened }

	public static class LeverAStateExt
    {
		public static bool IsClosed(this LeverAState self) => self == LeverAState.Closed;
		public static bool IsOpened(this LeverAState self) => self == LeverAState.Opened;
		
		public static LeverAState RewindState(this LeverAState self) => self switch
        {
			LeverAState.Closed => LeverAState.Opened,
			LeverAState.Opened => LeverAState.Closed,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
