using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(GearTypeC)), CanEditMultipleObjects]
	public class GearTypeCEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<Path> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<Path>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(GearTypeC path, GizmoType gizmo) =>
			drawLine(path);

		static void drawLine(GearTypeC gearTypeC) {
			if (gearTypeC.point__EDITOR.pathId == null || gearTypeC.point__EDITOR.pathId.isEmpty) return;
			var path = paths.FirstOrDefault(p => p.id_EDITOR == gearTypeC.point__EDITOR.pathId);

			if (path != null && gearTypeC.point__EDITOR.index >= 0 && gearTypeC.point__EDITOR.index < path.length_EDITOR) {
				var from = gearTypeC.transform.position;
				var point = path.at_EDITOR(gearTypeC.point__EDITOR.index);
				var to = path.transform.position + (Vector3) point.localPosition;

				Handles.DrawBezier(from, to, from, to, Color.green, null, LineWidth);
			}
		}
	}
}
		
		