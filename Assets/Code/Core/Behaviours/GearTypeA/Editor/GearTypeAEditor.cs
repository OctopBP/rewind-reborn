using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(GearTypeA)), CanEditMultipleObjects]
	public class GearTypeAEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<WalkPath> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(GearTypeA path, GizmoType gizmo) =>
			drawLine(path);

		static void drawLine(GearTypeA gearTypeA) {
			if (gearTypeA.point__EDITOR.pathId == null || gearTypeA.point__EDITOR.pathId.isEmpty) return;
			var path = paths.FirstOrDefault(p => p.id_EDITOR == gearTypeA.point__EDITOR.pathId);

			if (path != null && gearTypeA.point__EDITOR.index >= 0 && gearTypeA.point__EDITOR.index < path.length_EDITOR) {
				var from = gearTypeA.transform.position;
				var point = path.at_EDITOR(gearTypeA.point__EDITOR.index);
				var to = path.transform.position + (Vector3) point.localPosition;

				Handles.DrawBezier(from, to, from, to, Color.green, null, LineWidth);
			}
		}
	}
}
		
		