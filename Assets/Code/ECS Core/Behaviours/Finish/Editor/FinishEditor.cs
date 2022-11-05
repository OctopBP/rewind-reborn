using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(Finish)), CanEditMultipleObjects]
	public class FinishEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<Path> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<Path>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(Finish finish, GizmoType gizmo) =>
			drawLine(finish);

		static void drawLine(Finish finish) {
			if (finish.point__EDITOR.pathId == null || finish.point__EDITOR.pathId.isEmpty) return;
			var path = paths.FirstOrDefault(p => p.id_EDITOR == finish.point__EDITOR.pathId);

			if (path != null && finish.point__EDITOR.index >= 0 && finish.point__EDITOR.index < path.length_EDITOR) {
				var from = finish.transform.position;
				var point = path.at_EDITOR(finish.point__EDITOR.index);
				var to = path.transform.position + (Vector3) point.localPosition;

				var color = Color.green;
				Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
			}
		}
	}
}
		
		