using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(LeverABehaviour)), CanEditMultipleObjects]
	public class LeverABehaviourEditor : OdinEditor {
			const float LineWidth = 7f;
			static List<PathBehaviour> paths = new();

			protected override void OnEnable() {
				base.OnEnable();
				paths = FindObjectsOfType<PathBehaviour>().ToList();
			}

			[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
			public static void RenderCustomGizmos(LeverABehaviour leverBehaviour, GizmoType gizmo) =>
				drawLine(leverBehaviour);

			static void drawLine(LeverABehaviour leverBehaviour) { 
				var pointIndex = leverBehaviour.getPointIndex;
				var path = paths.FirstOrDefault(p => p.id == pointIndex.pathId);

				if (path != null && pointIndex.index >= 0 && pointIndex.index < path.length) {
					var from = leverBehaviour.transform.position;
					var point = path[pointIndex.index];
					var to = path.transform.position + (Vector3) point.position;

					var color = leverBehaviour.id.randomColor();
					Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
				}
			}
	}
}
		
		