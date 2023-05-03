using System;

namespace Rewind.SharedData {
	[Flags]
	public enum PointOpenStatus : short {
		Opened = 0,
		ClosedLeft = 1,
		ClosedRight = 2
	}

	public static class PointOpenStatusExt {
		public static bool isOpenRight(this PointOpenStatus self) => !self.HasFlag(PointOpenStatus.ClosedRight);
		public static bool isOpenLeft(this PointOpenStatus self) => !self.HasFlag(PointOpenStatus.ClosedLeft);
		public static bool isOpened(this PointOpenStatus self) => self == PointOpenStatus.Opened;
		
		/// <summary>
		/// Checks if we can move from point with this open status.
		/// </summary>
		/// <returns>`true` if point is open from move direction</returns>
		public static bool openToMoveFromWithDirection(this PointOpenStatus self, HorizontalMoveDirection direction) =>
			direction switch {
				HorizontalMoveDirection.Left => self.isOpenLeft(),
				HorizontalMoveDirection.Right => self.isOpenRight(),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
		
		/// <summary>
		/// Checks if we can move to point with this open status.
		/// </summary>
		/// <returns>`true` if point is open to move direction</returns>
		public static bool openToMoveToWithDirection(this PointOpenStatus self, HorizontalMoveDirection direction) =>
			direction switch {
				HorizontalMoveDirection.Left => self.isOpenRight(),
				HorizontalMoveDirection.Right => self.isOpenLeft(),
				_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
			};
	}
}
