using UnityEngine;

namespace Rewind.Services {
	public class UnityInputService : IInputService {
		readonly Button rightButton;
		readonly Button leftButton;
		readonly Button interactButton;
		readonly Button rewindButton;

		public UnityInputService() {
			rightButton = new(KeyCode.D);
			leftButton = new(KeyCode.A);
			interactButton = new(KeyCode.E);
			rewindButton = new(KeyCode.T);
		}

		public bool getMoveRightButton() => rightButton.getButton();
		public bool getMoveRightButtonDown() => rightButton.getButtonDown();
		public bool getMoveRightButtonUp() => rightButton.getButtonUp();

		public bool getMoveLeftButton() => leftButton.getButton();
		public bool getMoveLeftButtonDown() => leftButton.getButtonDown();
		public bool getMoveLeftButtonUp() => leftButton.getButtonUp();

		public bool getInteractButton() => interactButton.getButton();
		public bool getInteractButtonDown() => interactButton.getButtonDown();
		public bool getInteractButtonUp() => interactButton.getButtonUp();

		public bool getRewindButton() => rewindButton.getButton();
		public bool getRewindButtonDown() => rewindButton.getButtonDown();
		public bool getRewindButtonUp() => rewindButton.getButtonUp();

		class Button {
			readonly KeyCode keyCode;

			public Button(KeyCode keyCode) {
				this.keyCode = keyCode;
			}

			public bool getButton() => Input.GetKey(keyCode);
			public bool getButtonDown() => Input.GetKeyDown(keyCode);
			public bool getButtonUp() => Input.GetKeyUp(keyCode);
		}
	}
}