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
			if (pathBehaviour.length > 0) {
				var color = pathBehaviour.id.randomColor();
				var pos = pathBehaviour.getPosition(0) + Vector3.up * .5f + Vector3.left * 0.2f;
				Handles.Label(pos, $"{pathBehaviour.name}", statesLabel(color));
			}
			drawLines(pathBehaviour, withText);
			drawPoints(pathBehaviour);
		}

		static void drawLines(PathBehaviour pathBehaviour, bool withText) {
			var pathColor = pathBehaviour.id.randomColor();
			for (var i = 0; i < pathBehaviour.length - 1; i++) {
				var pathOpen = pathBehaviour[i].status.isOpenRight() && pathBehaviour[i + 1].status.isOpenLeft();
				var from = pathBehaviour.transform.position + (Vector3) pathBehaviour[i].position;
				var to = pathBehaviour.transform.position + (Vector3) pathBehaviour[i + 1].position;

				var color = pathColor.withAlpha(pathOpen ? 1 : .4f);
				Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
				
				if (withText) {
					var pos = (from + to) * .5f + Vector3.down * .2f;
					var distance = (from - to).magnitude.abs();
					var (textSuffix, labelColor) = distance > 0.8f ? ("!", Color.red) : ("", Color.white);
					Handles.Label(pos, $"{distance:F1}{textSuffix}", statesLabel(labelColor));
				}
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
			var depth = pathBehaviour[i].depth;
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
				fontSize = IndexFontSize,
				normal = { textColor = new(.4f, .8f, .35f) },
				alignment = TextAnchor.MiddleCenter
			};

			var depthText = depth switch {
				< 0 => $" ({depth})",
				> 0 => $" (+{depth})",
				_ => "",
			};

			Handles.Label(pathPosition + newPos + Vector3.down * .2f, $"{i}{depthText}", labelTextStyle);
		}
	}
}