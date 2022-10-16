using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(GearTypeABehaviour)), CanEditMultipleObjects]
	public class GearTypeABehaviourEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<PathBehaviour> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<PathBehaviour>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(GearTypeABehaviour pathBehaviour, GizmoType gizmo) =>
			drawLine(pathBehaviour);

		static void drawLine(GearTypeABehaviour gearTypeABehaviour) {
			if (gearTypeABehaviour.point.pathId == null || gearTypeABehaviour.point.pathId.empty) return;
			var path = paths.FirstOrDefault(p => p.id_EDITOR == gearTypeABehaviour.point.pathId);

			if (path != null && gearTypeABehaviour.point.index >= 0 && gearTypeABehaviour.point.index < path.length_EDITOR) {
				var from = gearTypeABehaviour.transform.position;
				var point = path.at_EDITOR(gearTypeABehaviour.point.index);
				var to = path.transform.position + (Vector3) point.localPosition;

				Handles.DrawBezier(from, to, from, to, Color.green, null, LineWidth);
			}
		}
	}
}
		
		