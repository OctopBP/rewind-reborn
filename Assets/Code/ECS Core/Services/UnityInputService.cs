using UnityEngine;

namespace Rewind.Services {
	public class UnityInputService : IInputService {
		readonly Button rightButton;
		readonly Button leftButton;
		readonly Button upButton;
		readonly Button downButton;
		readonly Button interactButton;
		readonly Button rewindButton;

		public UnityInputService() {
			rightButton = new(KeyCode.D);
			leftButton = new(KeyCode.A);
			upButton = new(KeyCode.W);
			downButton = new(KeyCode.S);
			interactButton = new(KeyCode.E);
			rewindButton = new(KeyCode.T);
		}

		public bool getMoveRightButton() => rightButton.getButton();
		public bool getMoveRightButtonDown() => rightButton.getButtonDown();
		public bool getMoveRightButtonUp() => rightButton.getButtonUp();

		public bool getMoveLeftButton() => leftButton.getButton();
		public bool getMoveLeftButtonDown() => leftButton.getButtonDown();
		public bool getMoveLeftButtonUp() => leftButton.getButtonUp();

		public bool getMoveUpButton() => upButton.getButton();
		public bool getMoveUpButtonDown() => upButton.getButtonDown();
		public bool getMoveUpButtonUp() => upButton.getButtonUp();

		public bool getMoveDownButton() => downButton.getButton();
		public bool getMoveDownButtonDown() => downButton.getButtonDown();
		public bool getMoveDownButtonUp() => downButton.getButtonUp();

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