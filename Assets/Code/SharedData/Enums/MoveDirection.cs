using System;
using ExhaustiveMatching;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Rewind.SharedData {
	public enum MoveDirection : short { Left, Right, Up, Down }

	public static class MoveDirectionExt {
		public static Option<HorizontalMoveDirection> asHorizontal(this MoveDirection self) => self switch {
			MoveDirection.Left => HorizontalMoveDirection.Left,
			MoveDirection.Right => HorizontalMoveDirection.Right,
			MoveDirection.Up => None,
			MoveDirection.Down => None,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static Option<VerticalMoveDirection> asVertical(this MoveDirection self) => self switch {
			MoveDirection.Left => None,
			MoveDirection.Right => None,
			MoveDirection.Up => VerticalMoveDirection.Up,
			MoveDirection.Down => VerticalMoveDirection.Down,
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
			_ => throw ExhaustiveMatch.Failed(self)
		};
		
		public static void foldByAxis(
			this MoveDirection self, Action<HorizontalMoveDirection> onHorizontal,
			Action<VerticalMoveDirection> onVertical
		) => self.asHorizontal().Match(onHorizontal, () => self.asVertical().IfSome(onVertical));

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
		
		public static T match<T>(
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