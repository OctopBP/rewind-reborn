using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Rewind.ECSCore.Editor
{
	[CustomEditor(typeof(GearTypeC)), CanEditMultipleObjects]
	public class GearTypeCEditor : OdinEditor
	{
		private static List<WalkPath> paths = new();
		
		protected override void OnEnable()
		{
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void RenderCustomGizmos(GearTypeC path, GizmoType gizmo) =>
			DrawLine(path);

		private static void DrawLine(GearTypeC gearTypeC) =>
			WalkPathEditorExt.DrawLine(gearTypeC.transform, paths, gearTypeC.Point__EDITOR);
	}
}
		
		