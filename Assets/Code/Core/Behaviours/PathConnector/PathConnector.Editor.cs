#if UNITY_EDITOR
namespace Rewind.Behaviours {
	public partial class PathConnector {
		public PathPoint getPoint1__EDITOR => twoPointsWithDirection.point1;
		public PathPoint getPoint2__EDITOR => twoPointsWithDirection.point2;
	}
}
#endif