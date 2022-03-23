using System;

namespace Rewind.ECSCore.Enums {
	public enum MoveDirection : short { Left, Right }

	public static class MoveDirectionExt {
		public static int intValue(this MoveDirection self) => self switch {
			MoveDirection.Left => -1,
			MoveDirection.Right => 1,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};

		public static T map<T>(this MoveDirection self, T onLeft, T onRight, T @default = default) =>
			self switch {
				MoveDirection.Left => onLeft,
				MoveDirection.Right => onRight,
				_ => @default
			};
	}
}