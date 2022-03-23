using Rewind.ECSCore.Enums;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(PathBehaviour))]
	public class PathBehaviourEditor : OdinEditor {
		const float LineWidth = 7f;
		const float LineSize = .17f;

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(PathBehaviour pathBehaviour, GizmoType gizmo) {
			drawLines(pathBehaviour);
			drawPoints(pathBehaviour);
		}

		static void drawLines(PathBehaviour pathBehaviour) {
			for (var i = 0; i < pathBehaviour.length - 1; i++) {
				var pathOpen = pathBehaviour[i].status.isOpenRight() && pathBehaviour[i + 1].status.isOpenLeft();
				var color = pathOpen ? Color.green : Color.red;
				Gizmos.color = color;

				var from = pathBehaviour[i].position;
				var to = pathBehaviour[i + 1].position;

				Handles.DrawBezier(
					from, to, from, to, color, null, LineWidth
				);
			}
		}

		static void drawPoints(PathBehaviour pathBehaviour) {
			for (var i = 0; i < pathBehaviour.length; i++) {
				drawPoint(pathBehaviour, i);
			}
		}

		static void drawPoint(PathBehaviour pathBehaviour, int i) {
			var pointOpened = pathBehaviour[i].status.isOpened();
			Handles.color = pointOpened ? Color.green : Color.red;	

			var newPos = Handles.FreeMoveHandle(
				pathBehaviour[i].position, Quaternion.identity,
				LineSize, Vector3.zero, Handles.CylinderHandleCap
			);

			if (newPos != (Vector3) pathBehaviour[i].position) {
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