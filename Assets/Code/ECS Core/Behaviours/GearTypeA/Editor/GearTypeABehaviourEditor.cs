using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(GearTypeABehaviour)), CanEditMultipleObjects]
	public class GearTypeABehaviourEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<PathBehaviour> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<PathBehaviour>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(GearTypeABehaviour pathBehaviour, GizmoType gizmo) =>
			drawLine(pathBehaviour);

		static void drawLine(GearTypeABehaviour pathBehaviour) {
			if (pathBehaviour.point.pathId == null || pathBehaviour.point.pathId.empty) return;
			var path = paths.FirstOrDefault(p => p.id == pathBehaviour.point.pathId);

			if (path != null && pathBehaviour.point.index >= 0 && pathBehaviour.point.index < path.length) {
				var from = pathBehaviour.transform.position;
				var point = path[pathBehaviour.point.index];
				var to = path.transform.position + (Vector3) point.position;

				var color = pathBehaviour.id.randomColor();
				Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
			}
		}
	}
}
		
		