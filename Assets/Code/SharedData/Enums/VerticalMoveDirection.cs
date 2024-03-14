using ExhaustiveMatching;

namespace Rewind.SharedData
{
	public enum VerticalMoveDirection : short { Up, Down }
	
	public static class VerticalMoveDirectionExt
    {
		public static bool IsUp(this VerticalMoveDirection self) => self == VerticalMoveDirection.Up;
		public static bool IsDown(this VerticalMoveDirection self) => self == VerticalMoveDirection.Down;

		public static MoveState AsMoveState(this VerticalMoveDirection self) => self switch
        {
			VerticalMoveDirection.Up => MoveState.Up,
			VerticalMoveDirection.Down => MoveState.Down,
			_ => MoveState.None
		};
		
		public static int INTValue(this VerticalMoveDirection self) =>
			self switch
            {
				VerticalMoveDirection.Up => 1,
				VerticalMoveDirection.Down => -1,
				_ => throw ExhaustiveMatch.Failed(self)
			};
	}
}