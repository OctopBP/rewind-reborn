using UnityEngine;

namespace Rewind.Services {
	public class UnityInputService : IInputService {
		public bool getMoveRightButton() => Input.GetKey(KeyCode.D);
		public bool getMoveRightButtonDown() => Input.GetKeyDown(KeyCode.D);
		public bool getMoveRightButtonUp() => Input.GetKeyUp(KeyCode.D);

		public bool getMoveLeftButton() => Input.GetKey(KeyCode.A);
		public bool getMoveLeftButtonDown() => Input.GetKeyDown(KeyCode.A);
		public bool getMoveLeftButtonUp() => Input.GetKeyUp(KeyCode.A);

		public bool getInteractButton() => Input.GetKey(KeyCode.E);
		public bool getInteractButtonDown() => Input.GetKeyDown(KeyCode.E);
		public bool getInteractButtonUp() => Input.GetKeyUp(KeyCode.E);
	}
}