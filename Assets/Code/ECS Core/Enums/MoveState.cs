namespace Rewind.ECSCore.Enums {
	public enum MoveState : short { Moving, Standing }

	public static class MoveStateExt {
		public static bool isMoving(this MoveState self) => self == MoveState.Moving;
		public static bool isStanding(this MoveState self) => self == MoveState.Standing;
	}
}
