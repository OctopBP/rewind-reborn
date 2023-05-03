using System;

namespace Rewind.Behaviours {
	public enum ConnectorDirection { LeftToRight, RightToLeft, TopToBottom, BottomToTop }

	public static class ConnectorDirectionExt {
		public static (string first, string second) labels(this ConnectorDirection direction) => direction switch {
			ConnectorDirection.LeftToRight => ("Left", "Right"),
			ConnectorDirection.RightToLeft => ("Right", "Left"),
			ConnectorDirection.TopToBottom => ("Top", "Bottom"),
			ConnectorDirection.BottomToTop => ("Bottom", "Top"),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};
	}
}