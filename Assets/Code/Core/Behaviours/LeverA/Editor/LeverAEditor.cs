using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Rewind.ECSCore.Editor
{
	[CustomEditor(typeof(LeverA)), CanEditMultipleObjects]
	public class LeverAEditor : OdinEditor
	{
		private const float LineWidth = 3f;
		private static List<WalkPath> paths = new();

		protected override void OnEnable()
		{
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(LeverA lever, GizmoType gizmo) =>
			drawLine(lever);

		private static void drawLine(LeverA lever) =>
			WalkPathEditorExt.DrawLine(lever.transform, paths, lever.PointIndex__EDITOR);
	}
}
		
		