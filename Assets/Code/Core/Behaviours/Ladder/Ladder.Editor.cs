using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore {
	public partial class Ladder {
		[SerializeField, PublicAccessor] float stepHeight_EDITOR = .4f;
		
		[Button, InfoBox("All connections will be lost", InfoMessageType.Warning)]
		void distributeFromFirstToLast() {
			if (points.Count < 2) return;
			
			Undo.RecordObject(this, "Distribute from first to last");
			
			var first = points[0];
			var last = points[^1];
			var length = last._position - first._position;
			var count = length.magnitude / stepHeight_EDITOR;

			points = new();
			for (var i = 0; i < count; i++) {
				points.Add(new PointWithPosition(
					position: first._position + i * stepHeight_EDITOR * length.normalized,
					maybePathPoint: MaybePathPoint.none()
				));
			}
			points.Add(last);
		}
	}
}
