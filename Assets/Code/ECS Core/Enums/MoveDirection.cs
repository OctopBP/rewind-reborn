using System;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Rewind.ECSCore.Enums {
	public enum MoveDirection : short { Left, Right, Up, Down };

	public static class MoveDirectionExt {
		public static Option<HorizontalMoveDirection> asHorizontal(this MoveDirection self) => self switch {
			MoveDirection.Left => HorizontalMoveDirection.Left,
			MoveDirection.Right => HorizontalMoveDirection.Right,
			MoveDirection.Up => None,
			MoveDirection.Down => None,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
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
				MoveDirection.Up => 1,
				MoveDirection.Down => -1,
				_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static T foldByAxis<T>(this MoveDirection self, T onHorizontal, T onVertical) => self switch {
			MoveDirection.Left or MoveDirection.Right => onHorizontal,
			MoveDirection.Up or MoveDirection.Down => onVertical,
		};
		
		public static void foldByAxis(this MoveDirection self, Action onHorizontal, Action onVertical) =>
			(self.isHorizontal() ? onHorizontal : onVertical)?.Invoke();
		
		public static void foldByAxis(
			this MoveDirection self, Action<MoveDirection> onHorizontal, Action<MoveDirection> onVertical
		) => (self.isHorizontal() ? onHorizontal : onVertical)?.Invoke(self);

		public static T fold<T>(
			this MoveDirection self, T onLeft = default, T onRight = default,
			T onUp = default, T onDown = default, T @default = default
		) => self switch {
			MoveDirection.Left => onLeft ?? @default,
			MoveDirection.Right => onRight ?? @default,
			MoveDirection.Up => onUp ?? @default,
			MoveDirection.Down => onDown ?? @default,
			_ => @default
		};
		
		public static bool ableToGoFromPoint(this MoveDirection self, PointOpenStatus pointStatus) =>
			pointStatus switch {
				PointOpenStatus.Opened => self.isHorizontal(),
				PointOpenStatus.ClosedLeft => self.isRight(),
				PointOpenStatus.ClosedRight => self.isLeft(),
				_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
			};
		
		public static bool ableToGoToPoint(this MoveDirection self, PointOpenStatus pointStatus) => pointStatus switch {
			PointOpenStatus.Opened => self.isHorizontal(),
			PointOpenStatus.ClosedLeft => self.isLeft(),
			PointOpenStatus.ClosedRight => self.isRight(),
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static MoveState asMoveState(this MoveDirection self) => self switch {
			MoveDirection.Left => MoveState.Left,
			MoveDirection.Right => MoveState.Right,
			MoveDirection.Up => MoveState.Up,
			MoveDirection.Down => MoveState.Down,
			_ => MoveState.None
		};
	}
}