using ExhaustiveMatching;

namespace Rewind.Behaviours
{
	public enum PathConnectorDirection { LeftToRight, RightToLeft, TopToBottom, BottomToTop }

	public static class ConnectorDirectionExt
	{
		public static (string first, string second) Labels(this PathConnectorDirection direction) => direction switch
		{
			PathConnectorDirection.LeftToRight => ("Left", "Right"),
			PathConnectorDirection.RightToLeft => ("Right", "Left"),
			PathConnectorDirection.TopToBottom => ("Top", "Bottom"),
			PathConnectorDirection.BottomToTop => ("Bottom", "Top"),
			_ => throw ExhaustiveMatch.Failed(direction)
		};
	}
}