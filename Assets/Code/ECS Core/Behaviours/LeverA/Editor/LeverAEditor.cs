using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(LeverA)), CanEditMultipleObjects]
	public class LeverAEditor : OdinEditor {
			const float LineWidth = 7f;
			static List<Path> paths = new();

			protected override void OnEnable() {
				base.OnEnable();
				paths = FindObjectsOfType<Path>().ToList();
			}

			[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
			public static void renderCustomGizmos(LeverA lever, GizmoType gizmo) =>
				drawLine(lever);

			static void drawLine(LeverA lever) { 
				var pointIndex = lever.pointIndex__EDITOR;
				var path = paths.FirstOrDefault(p => p.id_EDITOR == pointIndex.pathId);

				if (path != null && pointIndex.index >= 0 && pointIndex.index < path.length_EDITOR) {
					var from = lever.transform.position;
					var point = path.at_EDITOR(pointIndex.index);
					var to = path.transform.position + (Vector3) point.localPosition;

					Handles.DrawBezier(from, to, from, to, Color.green, null, LineWidth);
				}
			}
	}
}
		
		