using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Rewind.ECSCore.Editor
{
	[CustomEditor(typeof(DoorA)), CanEditMultipleObjects]
	public class DoorAEditor : OdinEditor
    {
		private static List<WalkPath> paths = new();

			protected override void OnEnable()
            {
				base.OnEnable();
				paths = FindObjectsOfType<WalkPath>().ToList();
			}

			[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
			public static void RenderCustomGizmos(DoorA door, GizmoType gizmo) =>
				DrawLine(door);

			private static void DrawLine(DoorA door)
			{
				WalkPathEditorExt.DrawLine(door.transform, paths, door._rightPoint);
				WalkPathEditorExt.DrawLine(door.transform, paths, door._rightPoint.PathWithIndex(door._rightPoint.index - 1));
			}
	}
}
		
		