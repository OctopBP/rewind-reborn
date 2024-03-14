using System.Linq;
using Code.Helpers;
using Rewind.Extensions;
using Rewind.Extensions.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor
{
	[CustomEditor(typeof(Ladder))]
	public class LadderEditor : OdinEditor
	{
		private const float PointSize = .1f;
		private const float LadderWidth = .25f;
		private const float SideWidth = 10;
		private const float StairWidth = 5;

		private Ladder ladder;

		protected override void OnEnable()
		{
			base.OnEnable();
			ladder = target as Ladder;
			SceneView.duringSceneGui += Draw;
		}

		private void OnDestroy()
		{
			SceneView.duringSceneGui -= Draw;
		}

		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(Ladder ladder, GizmoType gizmo) => Draw(ladder);

		private void Draw(SceneView sceneView) => Draw(ladder);

		private static void Draw(Ladder ladder)
		{
			var ladderPosition = ladder.transform.position;
			var pointsWithConnections = ladder._points
				.Collect(_ => _._maybePathPoint.OptValue.Map(point => (position: _._position, point)));

			foreach (var (position, pathPoint) in pointsWithConnections)
			{
				Handles.color = ColorA.White;
				WalkPathEditorExt.DrawLine(ladderPosition + position.ToVector3(), WalkPathEditor.paths, pathPoint);
			}

			drawSteps(ladder);
		}

		private static void drawSteps(Ladder ladder)
		{
			var stairs = ladder._points.Select((point, i) =>
			{
				var maybeNextPoint = ladder._points.At(i + 1);
				var maybePrevPoint = ladder._points.At(i - 1);

				var dir = maybePrevPoint.Match(
					Some: prev => maybeNextPoint.Match(
						Some: next => next._position - prev._position,
						None: () => point._position - prev._position
					),
					None: () => maybeNextPoint.Match(
						Some: next => next._position - point._position,
						None: () => Vector2.up
					)
				);
				var normalCross = Vector2.Perpendicular(dir).ToVector3().normalized;
				return (point, normalCross);
			}).ToList();

			var ladderPos = ladder.transform.position;

			for (var i = 0; i < stairs.Count; i++)
			{
				var pointData = stairs[i];
				var nextNormalCross = stairs.At(i + 1).IfNone(pointData);
				
				var (point, normalCross) = pointData;
				var (nextPoint, nextCross) = nextNormalCross;

				var position = ladderPos + point._position.ToVector3();
				var nextPos = ladderPos + nextPoint._position.ToVector3();
				
				var topR = nextPos + nextCross * LadderWidth;
				var topL = nextPos - nextCross * LadderWidth;

				var centerR = position + normalCross * LadderWidth;
				var centerL = position - normalCross * LadderWidth;

				if (i < stairs.Count - 1)
				{
					HandlesExt.DrawLine(centerL, topL, SideWidth, ColorA.Brown);
					HandlesExt.DrawLine(centerR, topR, SideWidth, ColorA.Brown);
				}
				
				HandlesExt.DrawLine(centerL, centerR, StairWidth, ColorA.Brown);

				Handles.color = ColorA.Gray;
				Handles.Label(position + Vector3.up * .1f, $"{i}");

				var newPos = Handles.FreeMoveHandle(position, PointSize, Vector3.zero, Handles.CylinderHandleCap)
				             - ladderPos;

				if (newPos != point._position.ToVector3())
				{
					Undo.RecordObject(ladder, "Move ladder point");
					ladder._points[i].setPosition(newPos);
				}
			}
		}
	}
}