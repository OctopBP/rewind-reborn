using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using GUI = UnityEngine.GUI;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(Path))]
	public class PathEditor : OdinEditor {
		const float LineWidth = 7f;
		const float PointSize = .17f;
		const int FontSize = 9;
		const int IndexFontSize = 12;

		Path path;

		static GUIStyle statesLabel(Color color) => new(GUI.skin.label) {
			alignment = TextAnchor.LowerCenter,
			fontSize = FontSize,
			fontStyle = FontStyle.Bold,
			normal = new() {
				textColor = color
			}
		};

		protected override void OnEnable() {
			base.OnEnable();
			path = target as Path;
			SceneView.duringSceneGui += draw;
		}

		void OnDestroy() {
			SceneView.duringSceneGui -= draw;
		}

		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(Path path, GizmoType gizmo) =>
			draw(path, withText: false);

		void draw(SceneView sceneView) => draw(path, withText: true);

		static void draw(Path path, bool withText) {
			if (path.length_EDITOR > 0) {
				var color = ColorExtensions.randomColorForGuid(path.id_EDITOR);
				var pos = (Vector3) path.getWorldPosition(0) + Vector3.up * .5f + Vector3.left * 0.2f;
				Handles.Label(pos, $"{path.name}", statesLabel(color));
			}
			drawLines(path, withText);
			drawPoints(path);
		}

		static void drawLines(Path path, bool withText) {
			var pathColor = ColorExtensions.randomColorForGuid(path.id_EDITOR);
			for (var i = 0; i < path.length_EDITOR - 1; i++) {
				var currentPoint = path.at_EDITOR(i);
				var nextPoint = path.at_EDITOR(i + 1);

				var from = path.getWorldPosition(i);
				var to = path.getWorldPosition(i + 1);
				var pathOpen = currentPoint.status.isOpenRight() && nextPoint.status.isOpenLeft();

				var color = pathColor.withAlpha(pathOpen ? 1 : .4f);
				Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
				
				if (withText) {
					var pos = (from + to) * .5f + Vector2.down * .2f;
					var distance = (from - to).magnitude.abs();
					var (textSuffix, labelColor) = distance > 0.8f ? ("!", Color.red) : ("", Color.white);
					Handles.Label(pos, $"{distance:F1}{textSuffix}", statesLabel(labelColor));
				}
			}
		}

		static void drawPoints(Path path) {
			for (var i = 0; i < path.length_EDITOR; i++) {
				drawPoint(path, i);
			}
		}

		static void drawPoint(Path path, int i) {
			var pointOpened = path.at_EDITOR(i).status.isOpened();
			var depth = path.at_EDITOR(i).depth;
			Handles.color = pointOpened ? Color.green : Color.red;

			var newPos = Handles.FreeMoveHandle(
				path.getWorldPosition(i), Quaternion.identity,
				PointSize, Vector3.zero, Handles.CylinderHandleCap
			);

			if (newPos != (Vector3) path.getWorldPosition(i)) {
				Undo.RecordObject(path, "Move point");
				path.setWorldPosition_EDITOR(i, newPos);
			}

			var labelTextStyle = new GUIStyle {
				fontStyle = FontStyle.Bold,
				fontSize = IndexFontSize,
				normal = { textColor = new(.4f, .8f, .35f) },
				alignment = TextAnchor.MiddleCenter
			};

			var depthText = depth switch {
				< 0 => $" ({depth})",
				> 0 => $" (+{depth})",
				_ => "",
			};

			Handles.Label(newPos + Vector3.down * .2f, $"{i}{depthText}", labelTextStyle);
		}
	}
}