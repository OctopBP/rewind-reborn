using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(PuzzleGroupBehaviour)), CanEditMultipleObjects]
	public class PuzzleGroupBehaviourEditor : OdinEditor {
		const float Margin = 0.8f;

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(PuzzleGroupBehaviour puzzleGroup, GizmoType gizmo) {
			drawRect(puzzleGroup);
		}

		static void drawRect(PuzzleGroupBehaviour puzzleGroup) {
			var positions = new List<Vector3>();
			positions.AddRange(puzzleGroup.getInputs.Select(i => i.gameObject.transform.position));
			positions.AddRange(puzzleGroup.getOutputs.Select(i => i.gameObject.transform.position));

			if (positions.Any()) {
				var minX = positions.Min(p => p.x) - Margin;
				var minY = positions.Min(p => p.y) - Margin;
				var maxX = positions.Max(p => p.x) + Margin;
				var maxY = positions.Max(p => p.y) + Margin;

				var rect = new Rect(minX, minY, maxX - minX, maxY - minY);
				var color = puzzleGroup.guid.randomColor();
				Handles.DrawSolidRectangleWithOutline(rect, color.withAlpha(.05f), color.withAlpha(.5f));
			}
		}
	}
}

