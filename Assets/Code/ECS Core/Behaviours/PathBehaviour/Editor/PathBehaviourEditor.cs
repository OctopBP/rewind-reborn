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
		const float PointSize = .17f;
		const int FontSize = 9;
		const int IndexFontSize = 12;

		PathBehaviour pathBehaviour;

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
			pathBehaviour = target as PathBehaviour;
			SceneView.duringSceneGui += draw;
		}

		void OnDestroy() {
			SceneView.duringSceneGui -= draw;
		}

		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(PathBehaviour pathBehaviour, GizmoType gizmo) =>
			draw(pathBehaviour, withText: false);

		void draw(SceneView sceneView) => draw(pathBehaviour, withText: true);

		static void draw(PathBehaviour pathBehaviour, bool withText) {
			if (pathBehaviour.length_EDITOR > 0) {
				var color = ColorExtensions.randomColorForGuid(pathBehaviour.id_EDITOR);
				var pos = (Vector3) pathBehaviour.getWorldPosition(0) + Vector3.up * .5f + Vector3.left * 0.2f;
				Handles.Label(pos, $"{pathBehaviour.name}", statesLabel(color));
			}
			drawLines(pathBehaviour, withText);
			drawPoints(pathBehaviour);
		}

		static void drawLines(PathBehaviour pathBehaviour, bool withText) {
			var pathColor = ColorExtensions.randomColorForGuid(pathBehaviour.id_EDITOR);
			for (var i = 0; i < pathBehaviour.length_EDITOR - 1; i++) {
				var currentPoint = pathBehaviour.at_EDITOR(i);
				var nextPoint = pathBehaviour.at_EDITOR(i + 1);

				var from = pathBehaviour.getWorldPosition(i);
				var to = pathBehaviour.getWorldPosition(i + 1);
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

		static void drawPoints(PathBehaviour pathBehaviour) {
			for (var i = 0; i < pathBehaviour.length_EDITOR; i++) {
				drawPoint(pathBehaviour, i);
			}
		}

		static void drawPoint(PathBehaviour pathBehaviour, int i) {
			var pointOpened = pathBehaviour.at_EDITOR(i).status.isOpened();
			var depth = pathBehaviour.at_EDITOR(i).depth;
			Handles.color = pointOpened ? Color.green : Color.red;

			var newPos = Handles.FreeMoveHandle(
				pathBehaviour.getWorldPosition(i), Quaternion.identity,
				PointSize, Vector3.zero, Handles.CylinderHandleCap
			);

			if (newPos != (Vector3) pathBehaviour.getWorldPosition(i)) {
				Undo.RecordObject(pathBehaviour, "Move point");
				pathBehaviour.setWorldPosition_EDITOR(i, newPos);
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