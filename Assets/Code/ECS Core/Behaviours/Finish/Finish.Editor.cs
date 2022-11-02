#if UNITY_EDITOR
namespace Rewind.Behaviours {
	public partial class Finish {
		public PathPoint point__EDITOR => pointIndex;
	}
}
#endif