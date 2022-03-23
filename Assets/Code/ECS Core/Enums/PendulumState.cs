using System;

namespace Rewind.ECSCore.Enums {
	public enum PendulumState: short  { NotActive, Active }

	public static class PendulumStateExt {
		public static bool isNotActive(this PendulumState self) => self == PendulumState.NotActive;
		public static bool isActive(this PendulumState self) => self == PendulumState.Active;

		public static void fold(
			this PendulumState self, Action<PendulumState> onNotActive, Action<PendulumState> onActive
		) => (self switch {
			PendulumState.NotActive => onNotActive,
			PendulumState.Active => onActive,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		}).Invoke(self);
		
		public static T fold<T>(
			this PendulumState self, Func<PendulumState, T> onNotActive, Func<PendulumState, T> onActive
		) => (self switch {
			PendulumState.NotActive => onNotActive,
			PendulumState.Active => onActive,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		}).Invoke(self);
	}
}
