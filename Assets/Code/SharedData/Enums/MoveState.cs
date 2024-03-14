using System;

namespace Rewind.SharedData
{
	public enum MoveState : short { None, Left, Right, Up, Down };

	public static class MoveStateExt
    {
		public static bool IsLeft(this MoveState self) => self == MoveState.Left;
		public static bool IsRight(this MoveState self) => self == MoveState.Right;
		public static bool IsUp(this MoveState self) => self == MoveState.Up;
		public static bool IsDown(this MoveState self) => self == MoveState.Down;
		public static bool IsNone(this MoveState self) => self == MoveState.None;
		public static bool IsHorizontal(this MoveState self) => self is MoveState.Left or MoveState.Right;
		public static bool IsVertical(this MoveState self) => self is MoveState.Up or MoveState.Down;

		public static int INTValue(this MoveState self) =>
			self switch
            {
				MoveState.Left => -1,
				MoveState.Right => 1,
				MoveState.Up => 1,
				MoveState.Down => -1,
				_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};

		public static T Fold<T>(
			this MoveState self, T onLeft = default, T onRight = default,
			T onUp = default, T onDown = default, T @default = default
		) => self switch
            {
			MoveState.Left => onLeft ?? @default,
			MoveState.Right => onRight ?? @default,
			MoveState.Up => onUp ?? @default,
			MoveState.Down => onDown ?? @default,
			_ => @default
		};
	}
}