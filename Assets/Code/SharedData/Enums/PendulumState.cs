using System;

namespace Rewind.SharedData
{
	public enum PendulumState: short { NotActive, Active }

	public static class PendulumStateExt
    {
		public static bool IsNotActive(this PendulumState self) => self == PendulumState.NotActive;
		public static bool IsActive(this PendulumState self) => self == PendulumState.Active;

		public static void Fold(
			this PendulumState self, Action<PendulumState> onNotActive, Action<PendulumState> onActive
		) => (self switch
            {
			PendulumState.NotActive => onNotActive,
			PendulumState.Active => onActive,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		}).Invoke(self);
		
		public static T Fold<T>(
			this PendulumState self, Func<PendulumState, T> onNotActive, Func<PendulumState, T> onActive
		) => (self switch
            {
			PendulumState.NotActive => onNotActive,
			PendulumState.Active => onActive,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		}).Invoke(self);
	}
}
