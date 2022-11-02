using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Behaviours {
	[Serializable]
	public class TwoPointsWithDirection {
		public enum ConnectorDirection { LeftToRight, RightToLeft, TopToBottom, BottomToTop }
		
		[Space(10), SerializeField] ConnectorDirection direction;
		[LabelText("@" + nameof(label1)), SerializeField] public PathPoint point1;
		[LabelText("@" + nameof(label2)), SerializeField] public PathPoint point2;

		[Button] void swapPoints() => (point1, point2) = (point2, point1);

		string label1 => labels().first;
		string label2 => labels().second;
		
		public (string first, string second) labels() => direction switch {
			ConnectorDirection.LeftToRight => ("Left", "Right"),
			ConnectorDirection.RightToLeft => ("Right", "Left"),
			ConnectorDirection.TopToBottom => ("Top", "Bottom"),
			ConnectorDirection.BottomToTop => ("Bottom", "Top"),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};

		public PathPointsPare asPathPointsPare => new PathPointsPare(point1, point2);
	}
}