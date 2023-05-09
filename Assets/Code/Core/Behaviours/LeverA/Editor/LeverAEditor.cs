using System.Collections.Generic;
using System.Linq;
using Code.Helpers;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(LeverA)), CanEditMultipleObjects]
	public class LeverAEditor : OdinEditor {
			const float LineWidth = 3f;
			static List<WalkPath> paths = new();

			protected override void OnEnable() {
				base.OnEnable();
				paths = FindObjectsOfType<WalkPath>().ToList();
			}

			[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
			public static void renderCustomGizmos(LeverA lever, GizmoType gizmo) =>
				drawLine(lever);
			
			static void drawLine(LeverA lever) =>
				WalkPathEditorExt.drawLine(lever.transform, paths, lever.pointIndex__EDITOR);
	}
}
		
		