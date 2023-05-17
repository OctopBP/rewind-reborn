using System.Collections.Generic;
using Code.Helpers;
using Rewind.Extensions;
using Rewind.Extensions.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using GUI = UnityEngine.GUI;
using static TablerIcons.TablerIcons;

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
				var from = walkPath.getWorldPosition(i);
				var to = walkPath.getWorldPosition(i + 1);
				
				HandlesExt.drawLine(from, to, LineWidth, pathColor);

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
			var depth = point.depth;
			Handles.color = ColorA.green;

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

			drawPointLeftConnectorMoveStatus(point.leftPathStatus);

			void drawPointLeftConnectorMoveStatus(UnityLeftPathDirectionBlock leftConnectorMoveStatus) {
				var pos = newPos + Vector3.up * .1f + Vector3.left * 0.2f;
				leftConnectorMoveStatus.fold(
					onBlockToRight: () => HandlesExt.drawArrow(pos, .15f, LineWidth, ColorA.green, true),
					onBlockToLeft: () => HandlesExt.drawArrow(pos, .15f, LineWidth, ColorA.green),
					onBoth: () => HandlesExt.drawX(pos, .1f, LineWidth, ColorA.green)
				);
			}
		}
	}
	
	public static class WalkPathEditorExt {
		public static void drawLine(Transform transform, IEnumerable<WalkPath> paths, PathPoint pathPoint) {
			const float lineWidth = 3f;
			
			var maybePath = paths.findById(pathPoint.pathId);
			maybePath.IfSome(path => path.at_EDITOR(pathPoint.index).IfSome(point => { 
					var from = transform.position; 
					var to = path.transform.position + (Vector3) point.localPosition;
					HandlesExt.drawLine(from, to, lineWidth, ColorA.gray);
				})
			);
		}
		
		public static void drawPointIcon(IEnumerable<WalkPath> paths, PathPoint point, string iconName) =>
			drawPointIcon(paths, point, iconName, Vector2.zero);
		
		public static void drawPointIcon(
			IEnumerable<WalkPath> paths, PathPoint point, string iconName, Vector2 offset
		) => paths.findById(point.pathId)
			.Map(p => p.getWorldPosition(point.index)).IfSome(
				position => {
					var tempColor = Gizmos.color;
					var iconPos = position + offset;
					DrawIconGizmo(iconPos, iconName, ColorA.green);
					Gizmos.color = ColorA.gray;
					Gizmos.DrawLine(position, iconPos);
					Gizmos.color = tempColor;
				});
	}
}