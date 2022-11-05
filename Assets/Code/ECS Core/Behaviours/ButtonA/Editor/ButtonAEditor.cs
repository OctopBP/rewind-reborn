using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(ButtonA)), CanEditMultipleObjects]
	public class ButtonAEditor : OdinEditor {
		const float LineWidth = 7f;
		static List<Path> paths = new();

		static GUIStyle guiStyle => new(GUI.skin.label) {
			alignment = TextAnchor.LowerCenter,
			fontSize = 20,
			fontStyle = FontStyle.Bold,
			normal = new() { textColor = Color.white }
		};

		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<Path>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(ButtonA button, GizmoType gizmo) =>
			drawLine(button);

		static void drawLine(ButtonA button) { 
			var pointIndex = button.getPointIndex__EDITOR;
			var path = paths.FirstOrDefault(p => p.id_EDITOR == pointIndex.pathId);

			if (path != null && pointIndex.index >= 0 && pointIndex.index < path.length_EDITOR) {
				var from = button.transform.position;
				var point = path.at_EDITOR(pointIndex.index);
				var to = path.transform.position + (Vector3) point.localPosition;

				Handles.DrawBezier(from, to, from, to, Color.green, null, LineWidth);
			}
		}
	}
}
		
		