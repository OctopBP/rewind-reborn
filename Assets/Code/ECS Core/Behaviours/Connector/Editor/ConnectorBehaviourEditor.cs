using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using Rewind.Behaviours;
using Rewind.Extensions;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using GUI = UnityEngine.GUI;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(ConnectorBehaviour)), CanEditMultipleObjects]
	public class ConnectorBehaviourEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<PathBehaviour> paths = new();
		
		static GUIStyle distanceLabel => new(GUI.skin.label) {
			alignment = TextAnchor.LowerCenter,
			fontSize = 7,
			fontStyle = FontStyle.Bold
		};
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<PathBehaviour>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(ConnectorBehaviour connectorBehaviour, GizmoType gizmo) =>
			drawLine(connectorBehaviour);

		static void drawLine(ConnectorBehaviour connectorBehaviour) {
			if (connectorBehaviour.getPoint1.pathId == null || connectorBehaviour.getPoint1.pathId.empty) return;
			if (connectorBehaviour.getPoint2.pathId == null || connectorBehaviour.getPoint2.pathId.empty) return;

			var path1 = paths.FirstOrDefault(p => p.id == connectorBehaviour.getPoint1.pathId);
			var path2 = paths.FirstOrDefault(p => p.id == connectorBehaviour.getPoint2.pathId);

			var maybeFrom = getMaybeValue(connectorBehaviour.getPoint1.index, path1);
			var maybeTo = getMaybeValue(connectorBehaviour.getPoint2.index, path2);

			if (maybeTo.valueOut(out var to) && maybeFrom.valueOut(out var from)) {
				var distance = (from - to).magnitude.abs();
				var isOpen = distance <= connectorBehaviour.getActivateDistance;
				var color = isOpen ? Color.green : Color.red;
				Handles.DrawBezier(from, to, from, to, color, null, LineWidth);
				Handles.Label((from + to) * .5f, $"{distance:F1}", distanceLabel);
			}

			Option<Vector3> getMaybeValue(int index, PathBehaviour pathBehaviour) =>
				(pathBehaviour != null && index >= 0 && index < pathBehaviour.length)
					? pathBehaviour.transform.position + (Vector3) pathBehaviour[index].position
					: Option<Vector3>.None;
		}
	}
}