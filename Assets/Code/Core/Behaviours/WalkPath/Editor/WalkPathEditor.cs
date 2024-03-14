using System;
using System.Collections.Generic;
using Code.Helpers;
using Rewind.Extensions;
using Rewind.Extensions.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using GUI = UnityEngine.GUI;
using static TablerIcons.TablerIcons;

namespace Rewind.ECSCore.Editor
{
	[CustomEditor(typeof(WalkPath))]
	public class WalkPathEditor : OdinEditor
	{
		// TODO: init after compilation
		public static WalkPath[] paths = Array.Empty<WalkPath>();

		private const float LineWidth = 7f;
		private const float PointSize = .1f;
		private const int FontSize = 9;
		private const int IndexFontSize = 11;
		private const float BlockIconsGap = .2f;
		private const float BlockIconsSize = .1f;

		private WalkPath walkPath;

		private static GUIStyle statesLabel(Color color) => new(GUI.skin.label)
		{
			alignment = TextAnchor.LowerCenter,
			fontSize = FontSize,
			fontStyle = FontStyle.Bold,
			normal = new()
			{
				textColor = color
			}
		};

		protected override void OnEnable()
		{
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>();
			
			walkPath = target as WalkPath;
			SceneView.duringSceneGui += Draw;
		}

		private void OnDestroy()
		{
			SceneView.duringSceneGui -= Draw;
		}

		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(WalkPath walkPath, GizmoType gizmo) =>
			Draw(walkPath, withText: false);

		private void Draw(SceneView sceneView) => Draw(walkPath, withText: true);

		private static void Draw(WalkPath walkPath, bool withText)
        {
			if (walkPath.Length__Editor > 0) PathName(0, Vector3.right * 0.25f);
			if (walkPath.Length__Editor > 1) PathName(walkPath.Length__Editor - 1, Vector3.left * 0.25f);

			DrawLines(walkPath, withText);
			DrawPoints(walkPath);

			void PathName(int index, Vector3 horizontalOffset)
            {
				var color = ColorExtensions.RandomColorForGuid(walkPath._pathId);
				var offset = Vector3.up * .1f + horizontalOffset;
				var position = walkPath.GetWorldPositionOrThrow(index).WithZ(0);
				Handles.Label(position + offset, walkPath.name, statesLabel(color));
			}
		}

		private static void DrawLines(WalkPath walkPath, bool withText)
        {
			var pathColor = ColorExtensions.RandomColorForGuid(walkPath._pathId);
			for (var i = 1; i < walkPath.Length__Editor; i++)
            {
				var from = walkPath.GetWorldPositionOrThrow(i - 1);
				var to = walkPath.GetWorldPositionOrThrow(i);
				
				HandlesExt.DrawLine(from, to, LineWidth, pathColor);

				if (from.x > to.x)
				{
					var pos = (from + to) * .5f + Vector2.up;
					Handles.Label(
						pos, $"Point {i + 1}\nshould be on the right\nof point {i}!",
						statesLabel(ColorA.Red)
					);
				}
				
				if (withText)
				{
					var pos = (from + to) * .5f + Vector2.down * .2f;
					var distance = (from - to).magnitude.Abs();
					var (textSuffix, labelColor) = distance > 0.8f ? ("!", red: ColorA.Red) : ("", white: ColorA.White);
					Handles.Label(pos, $"{distance:F1}{textSuffix}", statesLabel(labelColor));
				}
			}
		}

		private static void DrawPoints(WalkPath walkPath)
        {
			for (var i = 0; i < walkPath.Length__Editor; i++)
            {
				DrawPoint(walkPath, i);
			}
		}

		private static void DrawPoint(WalkPath walkPath, int i)
		{
			var point = walkPath.at_EDITOR(i).GetOrThrow("");
			var depth = point.depth;
			Handles.color = ColorA.Gray;

			var position = walkPath.GetWorldPositionOrThrow(i).WithZ(0);
			var newPos = Handles.FreeMoveHandle(position, PointSize, Vector3.zero, Handles.CylinderHandleCap);

			if (newPos != position)
            {
				Undo.RecordObject(walkPath, "Move point");
				walkPath.setWorldPosition_EDITOR(i, newPos);
			}

			var labelTextStyle = new GUIStyle
            {
				fontStyle = FontStyle.Bold,
				fontSize = IndexFontSize,
				normal = { textColor = ColorA.Green },
				alignment = TextAnchor.MiddleCenter
			};

			var depthText = depth switch
			{
				< 0 => $" [{depth}]",
				> 0 => $" [+{depth}]",
				_ => "",
			};

			Handles.Label(newPos + Vector3.down * .2f, $"{i}{depthText}", labelTextStyle);

			walkPath.GetMaybeWorldPosition(i - 1).IfSome(
				prevPos => DrawPointLeftConnectorMoveStatus(prevPos, point.leftPathStatus)
			);

			void DrawPointLeftConnectorMoveStatus(
				Vector3 previousPosition, UnityLeftPathDirectionBlock leftConnectorMoveStatus
			)
			{
				var direction = previousPosition - newPos;
				var count = Math.Floor(0.5f + direction.magnitude / BlockIconsGap);
				for (var j = 1; j < count ; j++)
				{
					var pos = newPos + j * direction.normalized * BlockIconsGap;
					leftConnectorMoveStatus.Fold(
						onBlockToRight: () => HandlesExt.DrawArrowHeadL(pos, BlockIconsSize, LineWidth, ColorA.Orange),
						onBlockToLeft: () => HandlesExt.DrawArrowHeadR(pos, BlockIconsSize, LineWidth, ColorA.Orange),
						onBoth: () => HandlesExt.DrawX(pos, BlockIconsSize, LineWidth, ColorA.Red)
					);
				}
			}
		}
	}
	
	public static class WalkPathEditorExt
	{
		public static void DrawLine(Transform transform, IEnumerable<WalkPath> paths, PathPoint pathPoint) =>
			DrawLine(transform.position, paths, pathPoint);
		
		public static void DrawLine(Vector3 from, IEnumerable<WalkPath> paths, PathPoint pathPoint)
		{
			const float dashWidth = 3f;
			
			var maybePath = paths.FindById(pathPoint.pathId);
			maybePath.IfSome(path => path.at_EDITOR(pathPoint.index).IfSome(point =>
				{
					var to = path.transform.position + (Vector3) point.localPosition;
					Handles.color = ColorA.Gray;
					Handles.DrawDottedLine(from, to, dashWidth);
				})
			);
		}
		
		public static void DrawPointIcon(IEnumerable<WalkPath> paths, PathPoint point, string iconName) =>
			DrawPointIcon(paths, point, iconName, Vector2.zero);
		
		public static void DrawPointIcon(
			IEnumerable<WalkPath> paths, PathPoint point, string iconName, Vector2 offset
		) => paths.FindById(point.pathId)
			.FlatMap(p => p.GetMaybeWorldPosition(point.index)).IfSome(
				position =>
				{
					var tempColor = Gizmos.color;
					var iconPos = position + offset;
					DrawIconGizmo(iconPos, iconName, ColorA.Green);
					Gizmos.color = ColorA.Gray;
					Gizmos.DrawLine(position, iconPos);
					Gizmos.color = tempColor;
				});
	}
}