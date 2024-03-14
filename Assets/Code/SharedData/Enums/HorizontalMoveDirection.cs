using ExhaustiveMatching;
using static Rewind.SharedData.PoinLeftPathDirectionBlock;

namespace Rewind.SharedData
{
	public enum HorizontalMoveDirection : short { Left, Right };
	
	public static class HorizontalMoveDirectionExt
    {
		public static bool IsLeft(this HorizontalMoveDirection self) => self == HorizontalMoveDirection.Left;
		public static bool IsRight(this HorizontalMoveDirection self) => self == HorizontalMoveDirection.Right;

		public static CharacterLookDirection AsCharacterLookDirection(this HorizontalMoveDirection self) =>
			self switch
            {
				HorizontalMoveDirection.Left => CharacterLookDirection.Left,
				HorizontalMoveDirection.Right => CharacterLookDirection.Right,
				_ => throw ExhaustiveMatch.Failed(self)
			};
		
		public static int INTValue(this HorizontalMoveDirection self) =>
			self switch
            {
				HorizontalMoveDirection.Left => -1,
				HorizontalMoveDirection.Right => 1,
				_ => throw ExhaustiveMatch.Failed(self)
		};
		
		public static MoveState AsMoveState(this HorizontalMoveDirection self) => self switch
        {
			HorizontalMoveDirection.Left => MoveState.Left,
			HorizontalMoveDirection.Right => MoveState.Right,
			_ => MoveState.None
		};
		
		public static bool BlockedBy(
			this HorizontalMoveDirection self, LeftPathDirectionBlock leftPathDirectionBlock
		) => self switch
            {
			HorizontalMoveDirection.Left => leftPathDirectionBlock == LeftPathDirectionBlock.BlockToLeft,
			HorizontalMoveDirection.Right => leftPathDirectionBlock == LeftPathDirectionBlock.BlockToRight,
			_ => throw ExhaustiveMatch.Failed(self)
		};
	}
}