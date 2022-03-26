using System;

namespace Rewind.ECSCore.Enums {
	public enum PlatformAState: short  { NotActive, Active }

	public static class PlatformAStateExt {
		public static bool isNotActive(this PlatformAState self) => self == PlatformAState.NotActive;
		public static bool isActive(this PlatformAState self) => self == PlatformAState.Active;
		public static int sign(this PlatformAState self) => self switch {
			PlatformAState.NotActive => -1,
			PlatformAState.Active => 1,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
	}
}
