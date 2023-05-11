using System.Collections.Generic;
using System.Linq;
using Rewind.ECSCore;
using Rewind.ECSCore.Editor;
using Sirenix.OdinInspector.Editor;
using TablerIcons;
using UnityEditor;
using UnityEngine;

namespace Rewind.Behaviours {
	
	[CustomEditor(typeof(ActionTrigger)), CanEditMultipleObjects]
	public class ActionTriggerEditor : OdinEditor {
		static List<WalkPath> paths = new();

		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(ActionTrigger button, GizmoType gizmo) =>
			drawLine(button);

		static void drawLine(ActionTrigger actionTrigger) {
			WalkPathEditorExt.drawPointIcon(paths, actionTrigger._pointIndex, Icons.IconBolt, Vector2.up * 0.5f);
		}
	}
}