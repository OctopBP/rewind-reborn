using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(ButtonA)), CanEditMultipleObjects]
	public class ButtonAEditor : OdinEditor {
		static List<WalkPath> paths = new();

		static GUIStyle guiStyle => new(GUI.skin.label) {
			alignment = TextAnchor.LowerCenter,
			fontSize = 20,
			fontStyle = FontStyle.Bold,
			normal = new() { textColor = Color.white }
		};

		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(ButtonA button, GizmoType gizmo) =>
			drawLine(button);

		static void drawLine(ButtonA button) =>
			WalkPathEditorExt.drawLine(button.transform, paths, button.getPointIndex__EDITOR);
	}
}
		
		