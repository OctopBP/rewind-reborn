using System;

namespace Rewind.ECSCore.Enums {
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
	}
}
