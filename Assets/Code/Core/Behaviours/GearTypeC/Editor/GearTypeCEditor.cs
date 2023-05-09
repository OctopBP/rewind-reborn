using System.Collections.Generic;
using System.Linq;
using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(GearTypeC)), CanEditMultipleObjects]
	public class GearTypeCEditor : OdinEditor {
		static List<WalkPath> paths = new();
		
		protected override void OnEnable() {
			base.OnEnable();
			paths = FindObjectsOfType<WalkPath>().ToList();
		}

		[DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NotInSelectionHierarchy)]
		public static void renderCustomGizmos(GearTypeC path, GizmoType gizmo) =>
			drawLine(path);

		static void drawLine(GearTypeC gearTypeC) =>
			WalkPathEditorExt.drawLine(gearTypeC.transform, paths, gearTypeC.point__EDITOR);
	}
}
		
		