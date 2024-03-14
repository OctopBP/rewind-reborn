using System.Collections.Generic;
using System.Linq;
using Code.Helpers;
using Rewind.Behaviours;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor
{
	[CustomEditor(typeof(GearCPuzzleGroup)), CanEditMultipleObjects]
	public class GearCGearCPuzzleGroupEditor : OdinEditor
	{
		private const float Margin = 0.8f;
		private const float Radius = 6f;
		private const float Thickness = 40f;

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(GearCPuzzleGroup puzzleGroup, GizmoType gizmo)
		{
			drawRect(puzzleGroup);
		}

		private static void drawRect(GearCPuzzleGroup puzzleGroup)
		{
			var positions = new List<Vector3>();
			// positions.AddRange(puzzleGroup._inputs.Select(i => i.gameObject.transform.position));
			// positions.AddRange(puzzleGroup._outputs.Select(i => i.gameObject.transform.position));
			
			Handles.color = ColorA.Green.WithAlpha(.3f);
			foreach (var range in puzzleGroup.ranges)
			{
				Handles.DrawWireArc(
					center: puzzleGroup.transform.position, normal: Vector3.forward,
					from: Quaternion.AngleAxis(range.x, Vector3.forward) * Vector3.right,
					angle: range.y - range.x, radius: Radius, thickness: Thickness
				);
			}

			if (positions.Any())
			{
				var minX = positions.Min(p => p.x) - Margin;
				var minY = positions.Min(p => p.y) - Margin;
				var maxX = positions.Max(p => p.x) + Margin;
				var maxY = positions.Max(p => p.y) + Margin;

				var rect = new Rect(minX, minY, maxX - minX, maxY - minY);
				var color = ColorExtensions.RandomColorForGuid(puzzleGroup.id);
				Handles.DrawSolidRectangleWithOutline(rect, color.WithAlpha(.05f), color.WithAlpha(.5f));
			}
		}
	}
}

