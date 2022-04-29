using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(FinishBehaviour)), CanEditMultipleObjects]
	public class FinishBehaviourEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<PathBehaviour> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<PathBehaviour>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(FinishBehaviour finishBehaviour, GizmoType gizmo) =>
			drawLine(finishBehaviour);

		static void drawLine(FinishBehaviour finishBehaviour) {
			if (finishBehaviour.point.pathId == null || finishBehaviour.point.pathId.empty) return;
			var path = paths.FirstOrDefault(p => p.id == finishBehaviour.point.pathId);

			if (path != null && finishBehaviour.point.index >= 0 && finishBehaviour.point.index < path.length) {
				var from = finishBehaviour.transform.position;
				var point = path[finishBehaviour.point.index];
				var to = path.transform.position + (Vector3) point.position;

				var color = Color.green;
				Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
			}
		}
	}
}
		
		