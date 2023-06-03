using System.Linq;
using Code.Helpers;
using ExhaustiveMatching;
using LanguageExt.UnsafeValueAccess;
using Rewind.Extensions;
using Rewind.Extensions.Editor;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(Ladder))]
	public class LadderEditor : OdinEditor {
		const float PointSize = .1f;
		const float LadderWidth = .25f;
		const float SideWidth = 10;
		const float StairWidth = 5;
		
		Ladder ladder;

		protected override void OnEnable() {
			base.OnEnable();
			ladder = target as Ladder;
			SceneView.duringSceneGui += draw;
		}

		void OnDestroy() {
			SceneView.duringSceneGui -= draw;
		}

		[DrawGizmo(GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(Ladder ladder, GizmoType gizmo) => draw(ladder);

		void draw(SceneView sceneView) => draw(ladder);

		static void draw(Ladder ladder) {
			var ladderPosition = ladder.transform.position;
			var pointsWithConnections = ladder._points
				.Select(_ => _._maybePathPoint.optValue.Map(point => (position: _._position, point)))
				.Somes();
			
			foreach (var (position, pathPoint) in pointsWithConnections) {
				Handles.color = ColorA.white;
				WalkPathEditorExt.drawLine(ladderPosition + position.toVector3(), WalkPathEditor.paths, pathPoint);
			}

			drawSteps(ladder);
		}

		static void drawSteps(Ladder ladder) {
			var stairs = ladder._points.Select((point, i) => {
				var maybeNextPoint = ladder._points.at(i + 1);
				var maybePrevPoint = ladder._points.at(i - 1);

				var dir = (maybePrevPoint, maybeNextPoint) switch {
					var (prev, next) when prev.IsSome && next.IsSome => next.ValueUnsafe()._position - prev.ValueUnsafe()._position,
					var (prev, next) when prev.IsSome && next.IsNone => point._position - prev.ValueUnsafe()._position,
					var (prev, next) when prev.IsNone && next.IsSome => next.ValueUnsafe()._position - point._position,
					var (prev, next) when prev.IsNone && next.IsNone => Vector2.up,
					_ => throw ExhaustiveMatch.Failed((maybePrevPoint, maybeNextPoint))
				};
				var normalCross = Vector2.Perpendicular(dir).toVector3().normalized;
				return (point, normalCross);
			}).ToList();

			var ladderPos = ladder.transform.position;

			for (var i = 0; i < stairs.Count; i++) {
				var pointData = stairs[i];
				var nextNormalCross = stairs.at(i + 1).IfNone(pointData);
				
				var (point, normalCross) = pointData;
				var (nextPoint, nextCross) = nextNormalCross;

				var position = ladderPos + point._position.toVector3();
				var nextPos = ladderPos + nextPoint._position.toVector3();
				
				var topR = nextPos + nextCross * LadderWidth;
				var topL = nextPos - nextCross * LadderWidth;

				var centerR = position + normalCross * LadderWidth;
				var centerL = position - normalCross * LadderWidth;

				if (i < stairs.Count - 1) {
					HandlesExt.drawLine(centerL, topL, SideWidth, ColorA.brown);
					HandlesExt.drawLine(centerR, topR, SideWidth, ColorA.brown);
				}
				
				HandlesExt.drawLine(centerL, centerR, StairWidth, ColorA.brown);

				Handles.color = ColorA.gray;
				Handles.Label(position + Vector3.up * .1f, $"{i}");

				var newPos = Handles.FreeMoveHandle(position, PointSize, Vector3.zero, Handles.CylinderHandleCap)
				             - ladderPos;

				if (newPos != point._position.toVector3()) {
					Undo.RecordObject(ladder, "Move ladder point");
					ladder._points[i].setPosition(newPos);
				}
			}
		}
	}
}