#if UNITY_EDITOR
using LanguageExt;
using Rewind.Extensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class WalkPath {
		public int length_EDITOR => points.Count;
		public Option<PointData> at_EDITOR(int i) =>
			pointModels != null ? pointModels.at(i).Map(_ => _.pointData) : points.at(i);
		public void setWorldPosition_EDITOR(int i, Vector2 position) =>
			points[i].localPosition = position - transform.position.xy();

		[Button]
		void positionToPathCenter() {
			if (length_EDITOR == 0) return;

			Undo.RecordObject(transform, "Position to center");
			var center = (getWorldPosition(0) + getWorldPosition(length_EDITOR - 1)) / 2;
			var offset = transform.position.xy() - center;

			transform.position = center.withZ(0);

			for (var i = 0; i < length_EDITOR; i++) {
				points[i].localPosition += offset;
			}
		}
		
		[Button]
		void distributePoints() {
			if (length_EDITOR <= 2) return;

			Undo.RecordObject(transform, "Distribute points");
			var firstPoint = points[0].localPosition;
			var lastPoint = points[length_EDITOR - 1].localPosition;
			var step = (lastPoint - firstPoint) / (length_EDITOR - 1);

			for (var i = 0; i < length_EDITOR; i++) {
				points[i].localPosition = firstPoint + step * i;
			}
		}
		
		[Button]
		void distributePoints08() {
			if (length_EDITOR <= 2) return;

			Undo.RecordObject(transform, "Distribute points 0.8");
			var firstPoint = points[0].localPosition;
			var lastPoint = firstPoint + Vector2.right * (length_EDITOR - 1) * 0.8f;
			var step = (lastPoint - firstPoint) / (length_EDITOR - 1);

			for (var i = 0; i < length_EDITOR; i++) {
				points[i].localPosition = firstPoint + step * i;
			}
		}
	}
}
#endif