using System;

namespace Rewind.SharedData
{
	[Flags]
	public enum PointBlockedMoveDirection : short
    {
		None = 0,
		LeftToRightBlocked = 1,
		RightToLeftBlocked = 2,
		Both = LeftToRightBlocked + RightToLeftBlocked
	}

	public static class PointBlockedMoveDirectionExt
    {
		public static void Fold(
			this PointBlockedMoveDirection self, Action onNone = null, Action onLeftToRightBlocked = null,
			Action onRightToLeftBlocked = null, Action onBoth = null
		)
        {
			switch (self)
            {
				case PointBlockedMoveDirection.None: onNone?.Invoke(); return;
				case PointBlockedMoveDirection.LeftToRightBlocked: onLeftToRightBlocked?.Invoke(); return;
				case PointBlockedMoveDirection.RightToLeftBlocked: onRightToLeftBlocked?.Invoke(); return;
				case PointBlockedMoveDirection.Both: onBoth?.Invoke(); return;
				default: throw new ArgumentOutOfRangeException(nameof(self), self, null);
			}
		}
	}
}
