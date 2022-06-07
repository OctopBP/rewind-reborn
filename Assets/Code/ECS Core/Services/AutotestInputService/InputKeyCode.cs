using UnityEngine;

namespace Rewind.Services.Autotest {
	public enum InputType { Right, Left, Up, Down, Interact, InteractSecond, Rewind }

	public class InputKeyCode {

		public readonly	InputType type;
		public readonly KeyCode keyCode;
		public readonly Button button = new();

		public InputKeyCode(InputType type, KeyCode keyCode) {
			this.type = type;
			this.keyCode = keyCode;
		}
	}
}