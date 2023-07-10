using UnityEngine;

namespace Code.Helpers.Utils {
	public sealed class GUIBackgroundColor : GUI.Scope {
		readonly Color value;

		public GUIBackgroundColor(Color color) {
			value = GUI.backgroundColor;
			GUI.backgroundColor = color;
		}

		protected override void CloseScope() {
			GUI.backgroundColor = value;
		}
	}

	public sealed class GUIColor: GUI.Scope {
		readonly Color value;

		public GUIColor(Color color) {
			value = GUI.color;
			GUI.color = color;
		}

		protected override void CloseScope() {
			GUI.color = value;
		}
	}

	public sealed class GUIContentColor : GUI.Scope {
		readonly Color value;

		public GUIContentColor(Color color) {
			value = GUI.contentColor;
			GUI.contentColor = color;
		}

		protected override void CloseScope() {
			GUI.contentColor = value;
		}
	}
	
	public sealed class GUILayoutVertical : GUI.Scope {
		public GUILayoutVertical(params GUILayoutOption[] options) {
			GUILayout.BeginVertical(options);
		}

		protected override void CloseScope() {
			GUILayout.EndVertical();
		}
	}
	
	public sealed class GUILayoutHorizontal : GUI.Scope {
		public GUILayoutHorizontal(params GUILayoutOption[] options) {
			GUILayout.BeginHorizontal(options);
		}

		protected override void CloseScope() {
			GUILayout.EndHorizontal();
		}
	}
}