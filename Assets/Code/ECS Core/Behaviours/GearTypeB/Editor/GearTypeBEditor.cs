using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(GearTypeB)), CanEditMultipleObjects]
	public class GearTypeBEditor : OdinEditor {
		const float LineWidth = 5f;
		const float PointSize = .17f;

		static List<Path> paths = new();

		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<Path>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(GearTypeB gear, GizmoType gizmo) {
			// drawLine(gearBehaviour);
			// drawConnectors(gearBehaviour);
		}

		// static void drawLine(GearTypeBBehaviour gearBehaviour) {
		// 	var color = Color.yellow;
		// 	Handles.color = color;
		//
		// 	for (var i = 0; i < gearBehaviour.count; i++) {
		// 		var center = gearBehaviour.transform.position;
		// 		var position = Extensions.Extensions.pointOnCircle(
		// 			index: i, max: gearBehaviour.count, radius: gearBehaviour.radius,
		// 			degAngleOffset: gearBehaviour.transform.eulerAngles.z, center: center
		// 		);
		//
		// 		Handles.DrawBezier(center, position, center, position, color, null, LineWidth);
		// 		Handles.CylinderHandleCap(i,position, Quaternion.identity, PointSize, EventType.Repaint);
		// 	}
		// }

		// static void drawConnectors(GearTypeBBehaviour gearBehaviour) {
		// 	if (gearBehaviour.point.pathId == null || gearBehaviour.point.pathId.empty) return;
		//
		// 	var path = paths.FirstOrDefault(p => p.id == gearBehaviour.point.pathId);
		//
		// 	var color = Color.cyan;
		// 	Handles.color = color;
		//
		// 	for (var i = 0; i < gearBehaviour.pathes.Count; i++) {
		// 		var pos = gearBehaviour.pathes[i].position;
		// 		var ppos = path[gearBehaviour.point.index].position;
		//
		// 		Handles.DrawBezier(pos, ppos, pos, ppos, color, null, LineWidth);
		// 	}
		// }
	}
}