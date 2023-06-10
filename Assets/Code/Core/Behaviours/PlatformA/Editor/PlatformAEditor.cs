using Rewind.Behaviours;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace Rewind.ECSCore.Editor {
	[CustomEditor(typeof(PlatformA)), CanEditMultipleObjects]
	public class PlatformAEditor : OdinEditor {
		PlatformA platformA;
		float progress;

		protected override void OnEnable() {
			platformA = (PlatformA) target;
		}

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			EditorGUILayout.BeginVertical(SirenixGUIStyles.BoxContainer);
			EditorGUILayout.LabelField("EDITOR", SirenixGUIStyles.BoldTitle);
			progress = EditorGUILayout.Slider("Progress", progress, 0f, 1f);

			platformA._platformHandler.position = platformA._spline.EvaluatePosition(progress);
			EditorGUILayout.EndVertical();
		}
	}
}