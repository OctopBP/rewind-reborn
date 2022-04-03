using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(ConnectorBehaviour)), CanEditMultipleObjects]
	public class ConnectorBehaviourEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<PathBehaviour> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<PathBehaviour>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(ConnectorBehaviour connectorBehaviour, GizmoType gizmo) {
			drawLine(connectorBehaviour);
		}

		static void drawLine(ConnectorBehaviour connectorBehaviour) {
			if (connectorBehaviour.point1.pathId == null || connectorBehaviour.point1.pathId.empty) return;
			if (connectorBehaviour.point2.pathId == null || connectorBehaviour.point2.pathId.empty) return;

			var path1 = paths.FirstOrDefault(p => p.id == connectorBehaviour.point1.pathId);
			var path2 = paths.FirstOrDefault(p => p.id == connectorBehaviour.point2.pathId);

			if (path1 != null && path2 != null) {
				var from = connectorBehaviour.transform.position;

				if (connectorBehaviour.point1.index >= 0 && connectorBehaviour.point1.index < path1.length) {
					var point = path1[connectorBehaviour.point1.index];
					var to = path1.transform.position + (Vector3) point.position;

					var color = connectorBehaviour.state.isOpened() ? Color.green : Color.red;
					Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
					Handles.Label((from + to) * .5f, $"{(from - to).magnitude.abs():F}");
				}
			}
		}
	}
}