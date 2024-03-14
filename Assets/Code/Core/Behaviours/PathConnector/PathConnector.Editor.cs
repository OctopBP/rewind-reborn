#if UNITY_EDITOR
namespace Rewind.Behaviours
{
	public partial class PathConnector
	{
		public PathPoint GetPoint1__EDITOR => twoPointsWithDirection.point1;
		public PathPoint GetPoint2__EDITOR => twoPointsWithDirection.point2;
	}
}
#endif