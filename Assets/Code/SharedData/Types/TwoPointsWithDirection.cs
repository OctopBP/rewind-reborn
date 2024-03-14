using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Behaviours
{
	[Serializable]
	public class TwoPointsWithDirection
	{
		public enum ConnectorDirection { LeftToRight, RightToLeft, TopToBottom, BottomToTop }
		
		[Space(10), SerializeField] private ConnectorDirection direction;
		[LabelText("@" + nameof(Label1)), SerializeField] public PathPoint point1;
		[LabelText("@" + nameof(Label2)), SerializeField] public PathPoint point2;

		[Button]
		private void SwapPoints() => (point1, point2) = (point2, point1);

		private string Label1 => Labels().first;
		private string Label2 => Labels().second;
		
		public (string first, string second) Labels() => direction switch
		{
			ConnectorDirection.LeftToRight => ("Left", "Right"),
			ConnectorDirection.RightToLeft => ("Right", "Left"),
			ConnectorDirection.TopToBottom => ("Top", "Bottom"),
			ConnectorDirection.BottomToTop => ("Bottom", "Top"),
			_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
		};

		public PathPointsPare AsPathPointsPare =>
			direction is ConnectorDirection.LeftToRight or ConnectorDirection.BottomToTop 
				? new PathPointsPare(point1, point2)
				: new PathPointsPare(point2, point1);
	}
}