using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Rewind.ECSCore.Editor
{
	[CustomEditor(typeof(GearTypeA)), CanEditMultipleObjects]
	public class GearTypeAEditor : OdinEditor
	{
		private static List<WalkPath> paths = new();
		
		protected override void OnEnable()
		{
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(GearTypeA path, GizmoType gizmo) =>
			DrawLine(path);

		private static void DrawLine(GearTypeA gearTypeA) =>
			WalkPathEditorExt.DrawLine(gearTypeA.transform, paths, gearTypeA.Point__EDITOR);
	}
}
		
		