using System.Collections.Generic;
using Code.Helpers;
using LanguageExt;
using Rewind.Extensions;
using Rewind.SharedData;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using GUI = UnityEngine.GUI;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(WalkPath))]
	public class WalkPathEditor : OdinEditor {
		const float LineWidth = 7f;
		const float PointSize = .17f;
		const int FontSize = 9;
		const int IndexFontSize = 11;

		WalkPath walkPath;

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
			walkPath = target as WalkPath;
			SceneView.duringSceneGui += draw;
		}

		void OnDestroy() {
			SceneView.duringSceneGui -= draw;
		}

		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(WalkPath walkPath, GizmoType gizmo) =>
			draw(walkPath, withText: false);

		void draw(SceneView sceneView) => draw(walkPath, withText: true);

		static void draw(WalkPath walkPath, bool withText) {
			if (walkPath.length_EDITOR > 0) pathName(0, Vector3.right * 0.5f);
			if (walkPath.length_EDITOR > 1) pathName(walkPath.length_EDITOR - 1, Vector3.left * 0.5f);

			void pathName(int index, Vector3 horizontalOffset) {
				var color = ColorExtensions.randomColorForGuid(walkPath._pathId);
				var offset = Vector3.up * .1f + horizontalOffset;
				var pos = (Vector3) walkPath.getWorldPosition(index) + offset;
				Handles.Label(pos, walkPath.name, statesLabel(color));
			}
			drawLines(walkPath, withText);
			drawPoints(walkPath);
		}

		static void drawLines(WalkPath walkPath, bool withText) {
			var pathColor = ColorExtensions.randomColorForGuid(walkPath._pathId);
			for (var i = 0; i < walkPath.length_EDITOR - 1; i++) {
				var currentPoint = walkPath.at_EDITOR(i).getOrThrow("currentPoint should be defined");
				var nextPoint = walkPath.at_EDITOR(i + 1).getOrThrow("nextPoint should be defined");;

				var from = walkPath.getWorldPosition(i);
				var to = walkPath.getWorldPosition(i + 1);
				var pathOpen = currentPoint.status.isOpenRight() && nextPoint.status.isOpenLeft();

				var color = pathColor.withAlpha(pathOpen ? 1 : .4f);
				Handles.DrawBezier(from, to, from, to, color, null, LineWidth);

				if (from.x > to.x) {
					var pos = (from + to) * .5f + Vector2.up;
					Handles.Label(
						pos, $"Point {i + 1}\nshould be on the right\nof point {i}!",
						statesLabel(ColorA.red)
					);
				}
				
				if (withText) {
					var pos = (from + to) * .5f + Vector2.down * .2f;
					var distance = (from - to).magnitude.abs();
					var (textSuffix, labelColor) = distance > 0.8f ? ("!", ColorA.red) : ("", ColorA.white);
					Handles.Label(pos, $"{distance:F1}{textSuffix}", statesLabel(labelColor));
				}
			}
		}

		static void drawPoints(WalkPath walkPath) {
			for (var i = 0; i < walkPath.length_EDITOR; i++) {
				drawPoint(walkPath, i);
			}
		}

		static void drawPoint(WalkPath walkPath, int i) {
			var point = walkPath.at_EDITOR(i).getOrThrow("");
			var pointOpened = point.status.isOpened();
			var depth = point.depth;
			Handles.color = pointOpened ? ColorA.green : ColorA.red;

			var newPos = Handles.FreeMoveHandle(
				walkPath.getWorldPosition(i), Quaternion.identity,
				PointSize, Vector3.zero, Handles.CylinderHandleCap
			);

			if (newPos != (Vector3) walkPath.getWorldPosition(i)) {
				Undo.RecordObject(walkPath, "Move point");
				walkPath.setWorldPosition_EDITOR(i, newPos);
			}

			var labelTextStyle = new GUIStyle {
				fontStyle = FontStyle.Bold,
				fontSize = IndexFontSize,
				normal = { textColor = ColorA.green },
				alignment = TextAnchor.MiddleCenter
			};

			var depthText = depth switch {
				< 0 => $" [{depth}]",
				> 0 => $" [+{depth}]",
				_ => "",
			};

			Handles.Label(newPos + Vector3.down * .2f, $"{i}{depthText}", labelTextStyle);
		}
	}
	
	public static class WalkPathEditorExt {
		public static void drawLine(Transform transform, IEnumerable<WalkPath> paths, PathPoint pathPoint) {
			const float lineWidth = 3f;
			
			var maybePath = paths.findById(pathPoint.pathId);

			maybePath.IfSome(path => path.at_EDITOR(pathPoint.index).IfSome(point => { 
					var from = transform.position; 
					var to = path.transform.position + (Vector3) point.localPosition;
					Handles.DrawBezier(from, to, from, to, ColorA.gray, null, lineWidth); 
				})
			);
		}
	}
}