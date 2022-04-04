using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using GUI = UnityEngine.GUI;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(PathBehaviour))]
	public class PathBehaviourEditor : OdinEditor {
		const float LineWidth = 7f;
		const float ColorLineWidth = 25f;
		const float PointSize = .17f;

		static GUIStyle statesLabel() => new(GUI.skin.label) {
			alignment = TextAnchor.LowerCenter,
			fontSize = 10
		};

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(PathBehaviour pathBehaviour, GizmoType gizmo) {
			drawLines(pathBehaviour);
			drawPoints(pathBehaviour);
		}

		static void drawLines(PathBehaviour pathBehaviour) {
			var color = pathBehaviour.id.randomColor().withAlpha(0.5f);
			for (var i = 0; i < pathBehaviour.length - 1; i++) {
				var pathOpen = pathBehaviour[i].status.isOpenRight() && pathBehaviour[i + 1].status.isOpenLeft();
				var lineColor = pathOpen ? Color.green : Color.red;

				var from = pathBehaviour.transform.position + (Vector3) pathBehaviour[i].position;
				var to = pathBehaviour.transform.position + (Vector3) pathBehaviour[i + 1].position;

				Handles.DrawBezier(from, to, from, to, color, null, ColorLineWidth);
				Handles.DrawBezier(from, to, from, to, lineColor, null, LineWidth);
				Handles.Label((from + to) * .5f + Vector3.up * .5f, $"{(from - to).magnitude.abs():F}", statesLabel());
			}
		}

		static void drawPoints(PathBehaviour pathBehaviour) {
			for (var i = 0; i < pathBehaviour.length; i++) {
				drawPoint(pathBehaviour, i);
			}
		}

		static void drawPoint(PathBehaviour pathBehaviour, int i) {
			var pathPosition = pathBehaviour.transform.position;
			var pointOpened = pathBehaviour[i].status.isOpened();
			Handles.color = pointOpened ? Color.green : Color.red;

			var newPos = Handles.FreeMoveHandle(
				pathPosition + (Vector3) pathBehaviour[i].position, Quaternion.identity,
				PointSize, Vector3.zero, Handles.CylinderHandleCap
			) - pathPosition;

			if (newPos != (Vector3) pathBehaviour[i].position) {
				Undo.RecordObject(pathBehaviour, "Move point");
				pathBehaviour.setPosition(i, newPos);
			}

			var labelTextStyle = new GUIStyle {
				fontStyle = FontStyle.Bold,
				fontSize = 12,
				normal = {textColor = Color.green},
				alignment = TextAnchor.MiddleCenter
			};

			Handles.Label(pathPosition + newPos + Vector3.down * .2f, $"{i}", labelTextStyle);
		}
	}
}