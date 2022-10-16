using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(GearTypeCBehaviour)), CanEditMultipleObjects]
	public class GearTypeCBehaviourEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<PathBehaviour> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<PathBehaviour>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(GearTypeCBehaviour pathBehaviour, GizmoType gizmo) =>
			drawLine(pathBehaviour);

		static void drawLine(GearTypeCBehaviour gearTypeCBehaviour) {
			if (gearTypeCBehaviour.point.pathId == null || gearTypeCBehaviour.point.pathId.empty) return;
			var path = paths.FirstOrDefault(p => p.id_EDITOR == gearTypeCBehaviour.point.pathId);

			if (path != null && gearTypeCBehaviour.point.index >= 0 && gearTypeCBehaviour.point.index < path.length_EDITOR) {
				var from = gearTypeCBehaviour.transform.position;
				var point = path.at_EDITOR(gearTypeCBehaviour.point.index);
				var to = path.transform.position + (Vector3) point.localPosition;

				Handles.DrawBezier(from, to, from, to, Color.green, null, LineWidth);
			}
		}
	}
}
		
		