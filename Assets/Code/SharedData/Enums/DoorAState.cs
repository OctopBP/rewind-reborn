using System;

namespace Rewind.SharedData
{
	public enum DoorAState : short { Closed, Opened }

	public static class DoorAStateExt
	{
		public static bool IsClosed(this DoorAState self) => self == DoorAState.Closed;
		public static bool IsOpened(this DoorAState self) => self == DoorAState.Opened;
		
		public static DoorAState RewindState(this DoorAState self) => self switch
		{
			DoorAState.Closed => DoorAState.Opened,
			DoorAState.Opened => DoorAState.Closed,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
