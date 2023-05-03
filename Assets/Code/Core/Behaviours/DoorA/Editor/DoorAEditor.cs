using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(DoorA)), CanEditMultipleObjects]
	public class DoorAEditor : OdinEditor {
			const float LineWidth = 7f;
			static List<WalkPath> paths = new();

			protected override void OnEnable() {
				base.OnEnable();
				paths = FindObjectsOfType<WalkPath>().ToList();
			}

			[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
			public static void renderCustomGizmos(DoorA door, GizmoType gizmo) =>
				drawLine(door);

			static void drawLine(DoorA door) {
				foreach (var pointIndex in door.getPointsIndex) {
					var path = paths.FirstOrDefault(p => p.id_EDITOR == pointIndex.pathId);

					if (path != null && pointIndex.index >= 0 && pointIndex.index < path.length_EDITOR) {
						var from = door.transform.position;
						var point = path.at_EDITOR(pointIndex.index);
						var to = path.transform.position + (Vector3) point.localPosition;

						Handles.DrawBezier(from, to, from, to, Color.green, null, LineWidth);
					}
				}
			}
	}
}
		
		