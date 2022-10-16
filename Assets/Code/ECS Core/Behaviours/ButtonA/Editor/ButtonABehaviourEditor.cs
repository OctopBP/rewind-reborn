using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(ButtonABehaviour)), CanEditMultipleObjects]
	public class ButtonABehaviourEditor : OdinEditor {
			const float LineWidth = 7f;
			static List<PathBehaviour> paths = new();

			protected override void OnEnable() {
				base.OnEnable();
				paths = FindObjectsOfType<PathBehaviour>().ToList();
			}

			[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
			public static void RenderCustomGizmos(ButtonABehaviour buttonBehaviour, GizmoType gizmo) =>
				drawLine(buttonBehaviour);

			static void drawLine(ButtonABehaviour buttonBehaviour) { 
				var pointIndex = buttonBehaviour.getPointIndex;
				var path = paths.FirstOrDefault(p => p.id_EDITOR == pointIndex.pathId);

				if (path != null && pointIndex.index >= 0 && pointIndex.index < path.length_EDITOR) {
					var from = buttonBehaviour.transform.position;
					var point = path.at_EDITOR(pointIndex.index);
					var to = path.transform.position + (Vector3) point.localPosition;

					Handles.DrawBezier(from, to, from, to, Color.green, null, LineWidth);
				}
			}
	}
}
		
		