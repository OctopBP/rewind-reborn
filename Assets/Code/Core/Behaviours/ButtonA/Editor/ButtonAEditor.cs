using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Rewind.ECSCore.Editor
{
	[CustomEditor(typeof(ButtonA)), CanEditMultipleObjects]
	public class ButtonAEditor : OdinEditor
	{
		private static List<WalkPath> paths = new();

		private static GUIStyle guiStyle => new(GUI.skin.label)
		{
			alignment = TextAnchor.LowerCenter,
			fontSize = 20,
			fontStyle = FontStyle.Bold,
			normal = new() { textColor = Color.white }
		};

		protected override void OnEnable()
		{
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(ButtonA button, GizmoType gizmo) =>
			DrawLine(button);

		private static void DrawLine(ButtonA button) =>
			WalkPathEditorExt.DrawLine(button.transform, paths, button.GetPointIndex__EDITOR);
	}
}
		
		