using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(DoorABehaviour)), CanEditMultipleObjects]
	public class DoorABehaviourEditor : OdinEditor {
			const float LineWidth = 7f;
			static List<PathBehaviour> paths = new();

			protected override void OnEnable() {
				base.OnEnable();
				paths = FindObjectsOfType<PathBehaviour>().ToList();
			}

			[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
			public static void RenderCustomGizmos(DoorABehaviour doorBehaviour, GizmoType gizmo) =>
				drawLine(doorBehaviour);

			static void drawLine(DoorABehaviour doorBehaviour) {
				foreach (var pointIndex in doorBehaviour.getPointsIndex) {
					var path = paths.FirstOrDefault(p => p.id == pointIndex.pathId);

					if (path != null && pointIndex.index >= 0 && pointIndex.index < path.length) {
						var from = doorBehaviour.transform.position;
						var point = path[pointIndex.index];
						var to = path.transform.position + (Vector3) point.position;

						var color = doorBehaviour.id.randomColor();
						Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
					}
				}
			}
	}
}
		
		