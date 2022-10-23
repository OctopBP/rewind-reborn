#if UNITY_EDITOR
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class Path {
		public SerializableGuid id_EDITOR => pathId;
		public int length_EDITOR => points.Count;
		public PointData at_EDITOR(int i) => pointModels != null ? pointModels[i].pointData : points[i];
		public void setWorldPosition_EDITOR(int i, Vector2 position) => points[i].localPosition = position;
	}
}
#endif