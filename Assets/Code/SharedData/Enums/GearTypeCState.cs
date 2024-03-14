using System;

namespace Rewind.SharedData
{
	public enum GearTypeCState : short { Closed, RotationRight, RotationLeft }

	public static class GearTypeCStateExt
    {
		public static bool IsClosed(this GearTypeCState self) => self == GearTypeCState.Closed;
		public static bool IsRotationRight(this GearTypeCState self) => self == GearTypeCState.RotationRight;
		public static bool IsRotationLeft(this GearTypeCState self) => self == GearTypeCState.RotationLeft;

		public static int SpeedMultiplier(this GearTypeCState self) => self switch
        {
			GearTypeCState.RotationRight => -1,
			GearTypeCState.RotationLeft => 1,
			GearTypeCState.Closed => 0,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};

		public static GearTypeCState RewindState(this GearTypeCState self) => self switch
        {
			GearTypeCState.RotationRight => GearTypeCState.RotationLeft,
			GearTypeCState.RotationLeft => GearTypeCState.RotationRight,
			GearTypeCState.Closed => GearTypeCState.Closed,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};

		public static void Fold(
			this GearTypeCState self, Action<GearTypeCState> onRotationRight,
			Action<GearTypeCState> onRotationLeft, Action<GearTypeCState> onClosed
		) => (self switch
            {
			GearTypeCState.RotationRight => onRotationRight,
			GearTypeCState.RotationLeft => onRotationLeft,
			GearTypeCState.Closed => onClosed,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		}).Invoke(self);

		public static T Fold<T>(
			this GearTypeCState self, Func<GearTypeCState, T> onRotationRight,
			Func<GearTypeCState, T> onRotationLeft, Func<GearTypeCState, T> onClosed
		) => (self switch
            {
			GearTypeCState.RotationRight => onRotationRight,
			GearTypeCState.RotationLeft => onRotationLeft,
			GearTypeCState.Closed => onClosed,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		}).Invoke(self);
	}
}
