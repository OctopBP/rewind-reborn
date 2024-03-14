using System;

namespace Rewind.SharedData
{
	public enum PlatformAState: short { NotActive, Active }

	public static class PlatformAStateExt
    {
		public static bool IsNotActive(this PlatformAState self) => self == PlatformAState.NotActive;
		public static bool IsActive(this PlatformAState self) => self == PlatformAState.Active;
		public static int Sign(this PlatformAState self) => self switch
        {
			PlatformAState.NotActive => -1,
			PlatformAState.Active => 1,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
