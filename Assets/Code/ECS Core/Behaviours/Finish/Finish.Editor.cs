#if UNITY_EDITOR
namespace Rewind.Behaviours {
	public partial class Finish {
		public PathPointType point__EDITOR => pointIndex;
	}
}
#endif