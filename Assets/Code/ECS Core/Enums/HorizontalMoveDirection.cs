using System;

namespace Rewind.ECSCore.Enums {
	public enum HorizontalMoveDirection : short { Left, Right };
	
	public static class HorizontalMoveDirectionExt {
		public static bool isLeft(this HorizontalMoveDirection self) => self == HorizontalMoveDirection.Left;
		public static bool isRight(this HorizontalMoveDirection self) => self == HorizontalMoveDirection.Right;

		public static CharacterLookDirection asCharacterLookDirection(this HorizontalMoveDirection self) =>
			self switch {
				HorizontalMoveDirection.Left => CharacterLookDirection.Left,
				HorizontalMoveDirection.Right => CharacterLookDirection.Right,
				_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
			};
		
		public static int intValue(this HorizontalMoveDirection self) =>
			self switch {
				HorizontalMoveDirection.Left => -1,
				HorizontalMoveDirection.Right => 1,
				_ => throw new ArgumentOutOfRangeException(nameof(self), self, null)
		};
		
		public static MoveState asMoveState(this HorizontalMoveDirection self) => self switch {
			HorizontalMoveDirection.Left => MoveState.Left,
			HorizontalMoveDirection.Right => MoveState.Right,
			_ => MoveState.None
		};
	}
}