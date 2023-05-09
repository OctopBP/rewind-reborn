using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(GearTypeA)), CanEditMultipleObjects]
	public class GearTypeAEditor : OdinEditor {
		static List<WalkPath> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(GearTypeA path, GizmoType gizmo) =>
			drawLine(path);

		static void drawLine(GearTypeA gearTypeA) =>
			WalkPathEditorExt.drawLine(gearTypeA.transform, paths, gearTypeA.point__EDITOR);
	}
}
		
		