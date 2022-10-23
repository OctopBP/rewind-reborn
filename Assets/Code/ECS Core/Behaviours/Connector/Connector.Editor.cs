#if UNITY_EDITOR
namespace Rewind.Behaviours {
	public partial class Connector {
		public PathPointType getPoint1__EDITOR => twoPointsWithDirection.point1;
		public PathPointType getPoint2__EDITOR => twoPointsWithDirection.point2;
		public float getActivateDistance__EDITOR => activateDistance;
	}
}
#endif