using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(PathBehaviour))]
	public class PathBehaviourEditor : UnityEditor.Editor {
		const float LineWidth = 7f;
		const float LineSize = .17f;

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(PathBehaviour pathBehaviour, GizmoType gizmo) {
			drawLines(pathBehaviour);
			drawPoints(pathBehaviour);
		}

		static void drawLines(PathBehaviour pathBehaviour) {
			Gizmos.color = Color.green;

			for (var i = 0; i < pathBehaviour.length - 1; i++) {
				var from = pathBehaviour[i];
				var to = pathBehaviour[i + 1];

				Handles.DrawBezier(
					from, to, from, to, Color.green, null, LineWidth
				);
			}
		}

		static void drawPoints(PathBehaviour pathBehaviour) {
			Handles.color = Color.green;
			
			for (var i = 0; i < pathBehaviour.length; i++) {
				drawPoint(pathBehaviour, i);
			}
		}

		static void drawPoint(PathBehaviour pathBehaviour, int i) {
			var newPos = Handles.FreeMoveHandle(
				pathBehaviour[i], Quaternion.identity,
				LineSize, Vector3.zero, Handles.CylinderHandleCap
			);

			if (newPos != (Vector3) pathBehaviour[i]) {
				Undo.RecordObject(pathBehaviour, "Move point");
				pathBehaviour.setPosition(i, newPos);
			}

			var labelTextStyle = new GUIStyle {
				fontStyle = FontStyle.Bold,
				fontSize = 16,
				normal = {textColor = Color.white},
				alignment = TextAnchor.MiddleCenter
			};
			
			Handles.Label(newPos + Vector3.down * .4f, i.ToString(), labelTextStyle);
		}
	}
}