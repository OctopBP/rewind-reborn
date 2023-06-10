using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(DoorA)), CanEditMultipleObjects]
	public class DoorAEditor : OdinEditor {
		  static List<WalkPath> paths = new();

			protected override void OnEnable() {
				base.OnEnable();
				paths = FindObjectsOfType<WalkPath>().ToList();
			}

			[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
			public static void renderCustomGizmos(DoorA door, GizmoType gizmo) =>
				drawLine(door);

			static void drawLine(DoorA door) {
				WalkPathEditorExt.drawLine(door.transform, paths, door._rightPoint);
				WalkPathEditorExt.drawLine(door.transform, paths, door._rightPoint.pathWithIndex(door._rightPoint.index - 1));
			}
	}
}
		
		