using System;

namespace Rewind.ECSCore.Enums {
	public enum MoveDirection : short { Left, Right, Up, Down };

	public static class MoveDirectionExt {
		public static bool isLeft(this MoveDirection self) => self == MoveDirection.Left;
		public static bool isRight(this MoveDirection self) => self == MoveDirection.Right;
		public static bool isUp(this MoveDirection self) => self == MoveDirection.Up;
		public static bool isDown(this MoveDirection self) => self == MoveDirection.Down;
		public static bool isHorizontal(this MoveDirection self) => self is MoveDirection.Left or MoveDirection.Right;
		public static bool isVertical(this MoveDirection self) => self is MoveDirection.Up or MoveDirection.Down;

		public static int intValue(this MoveDirection self) =>
			self switch {
				MoveDirection.Left => -1,
				MoveDirection.Right => 1,
				_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static T mapByAxis<T>(this MoveDirection self, T onHorizontal, T onVertical) => self switch {
			MoveDirection.Left or MoveDirection.Right => onHorizontal,
			MoveDirection.Up or MoveDirection.Down => onVertical,
		};
		
		public static void mapByAxis(this MoveDirection self, Action onHorizontal, Action onVertical) =>
			(self.isHorizontal() ? onHorizontal : onVertical)?.Invoke();
		
		public static void mapByAxis(
			this MoveDirection self, Action<MoveDirection> onHorizontal, Action<MoveDirection> onVertical
		) => (self.isHorizontal() ? onHorizontal : onVertical)?.Invoke(self);

		public static T map<T>(
			this MoveDirection self, T onLeft = default, T onRight = default,
			T onUp = default, T onDown = default, T @default = default
		) => self switch {
			MoveDirection.Left => onLeft ?? @default,
			MoveDirection.Right => onRight ?? @default,
			MoveDirection.Up => onUp ?? @default,
			MoveDirection.Down => onDown ?? @default,
			_ => @default
		};
	}
}