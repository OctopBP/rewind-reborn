using ExhaustiveMatching;

namespace Rewind.SharedData {
	public enum VerticalMoveDirection : short { Up, Down }
	
	public static class VerticalMoveDirectionExt {
		public static bool isUp(this VerticalMoveDirection self) => self == VerticalMoveDirection.Up;
		public static bool isDown(this VerticalMoveDirection self) => self == VerticalMoveDirection.Down;

		public static MoveState asMoveState(this VerticalMoveDirection self) => self switch {
			VerticalMoveDirection.Up => MoveState.Up,
			VerticalMoveDirection.Down => MoveState.Down,
			_ => MoveState.None
		};
		
		public static int intValue(this VerticalMoveDirection self) =>
			self switch {
				VerticalMoveDirection.Up => 1,
				VerticalMoveDirection.Down => -1,
				_ => throw ExhaustiveMatch.Failed(self)
			};
	}
}