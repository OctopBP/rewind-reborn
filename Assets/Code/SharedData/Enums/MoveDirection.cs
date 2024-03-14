using System;
using ExhaustiveMatching;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Rewind.SharedData
{
	public enum MoveDirection : short { Left, Right, Up, Down }

	public static class MoveDirectionExt
    {
		public static Option<HorizontalMoveDirection> AsHorizontal(this MoveDirection self) => self switch
        {
			MoveDirection.Left => HorizontalMoveDirection.Left,
			MoveDirection.Right => HorizontalMoveDirection.Right,
			MoveDirection.Up => None,
			MoveDirection.Down => None,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static Option<VerticalMoveDirection> AsVertical(this MoveDirection self) => self switch
        {
			MoveDirection.Left => None,
			MoveDirection.Right => None,
			MoveDirection.Up => VerticalMoveDirection.Up,
			MoveDirection.Down => VerticalMoveDirection.Down,
			_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static bool IsLeft(this MoveDirection self) => self == MoveDirection.Left;
		public static bool IsRight(this MoveDirection self) => self == MoveDirection.Right;
		public static bool IsUp(this MoveDirection self) => self == MoveDirection.Up;
		public static bool IsDown(this MoveDirection self) => self == MoveDirection.Down;
		public static bool IsHorizontal(this MoveDirection self) => self is MoveDirection.Left or MoveDirection.Right;
		public static bool IsVertical(this MoveDirection self) => self is MoveDirection.Up or MoveDirection.Down;

		public static int INTValue(this MoveDirection self) =>
			self switch
            {
				MoveDirection.Left => -1,
				MoveDirection.Right => 1,
				MoveDirection.Up => 1,
				MoveDirection.Down => -1,
				_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static T FoldByAxis<T>(this MoveDirection self, T onHorizontal, T onVertical) => self switch
        {
			MoveDirection.Left => onHorizontal,
			MoveDirection.Right => onHorizontal,
			MoveDirection.Up => onVertical,
			MoveDirection.Down => onVertical,
			_ => throw ExhaustiveMatch.Failed(self)
		};
		
		public static void FoldByAxis(
			this MoveDirection self, Action<HorizontalMoveDirection> onHorizontal,
			Action<VerticalMoveDirection> onVertical
		) => self.AsHorizontal().Match(onHorizontal, () => self.AsVertical().IfSome(onVertical));

		public static T Fold<T>(
			this MoveDirection self, T onLeft = default, T onRight = default,
			T onUp = default, T onDown = default, T @default = default
		) => self switch
            {
			MoveDirection.Left => onLeft ?? @default,
			MoveDirection.Right => onRight ?? @default,
			MoveDirection.Up => onUp ?? @default,
			MoveDirection.Down => onDown ?? @default,
			_ => @default
		};
		
		public static T Match<T>(
			this MoveDirection self, T onLeft = default, T onRight = default,
			T onUp = default, T onDown = default, T @default = default
		) => self switch
            {
			MoveDirection.Left => onLeft ?? @default,
			MoveDirection.Right => onRight ?? @default,
			MoveDirection.Up => onUp ?? @default,
			MoveDirection.Down => onDown ?? @default,
			_ => @default
		};
	}
}