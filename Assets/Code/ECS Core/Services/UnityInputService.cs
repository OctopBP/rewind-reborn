using UnityEngine;

namespace Rewind.Services
{
	public class UnityInputService : IInputService
	{
		private readonly Button rightButton = new(KeyCode.D);
		private readonly Button leftButton = new(KeyCode.A);
		private readonly Button upButton = new(KeyCode.W);
		private readonly Button downButton = new(KeyCode.S);
		private readonly Button interactButton = new(KeyCode.E);
		private readonly Button interactSecondButton = new(KeyCode.Q);
		private readonly Button rewindButton = new(KeyCode.T);

		public bool GetMoveRightButton() => rightButton.GetButton();
		public bool GetMoveRightButtonDown() => rightButton.GetButtonDown();
		public bool GetMoveRightButtonUp() => rightButton.GetButtonUp();

		public bool GetMoveLeftButton() => leftButton.GetButton();
		public bool GetMoveLeftButtonDown() => leftButton.GetButtonDown();
		public bool GetMoveLeftButtonUp() => leftButton.GetButtonUp();

		public bool GetMoveUpButton() => upButton.GetButton();
		public bool GetMoveUpButtonDown() => upButton.GetButtonDown();
		public bool GetMoveUpButtonUp() => upButton.GetButtonUp();

		public bool GetMoveDownButton() => downButton.GetButton();
		public bool GetMoveDownButtonDown() => downButton.GetButtonDown();
		public bool GetMoveDownButtonUp() => downButton.GetButtonUp();

		public bool GetInteractButton() => interactButton.GetButton();
		public bool GetInteractButtonDown() => interactButton.GetButtonDown();
		public bool GetInteractButtonUp() => interactButton.GetButtonUp();

		public bool GetInteractSecondButton() => interactSecondButton.GetButton();
		public bool GetInteractSecondButtonDown() => interactSecondButton.GetButtonDown();
		public bool GetInteractSecondButtonUp() => interactSecondButton.GetButtonUp();

		public bool GetRewindButton() => rewindButton.GetButton();
		public bool GetRewindButtonDown() => rewindButton.GetButtonDown();
		public bool GetRewindButtonUp() => rewindButton.GetButtonUp();

		private class Button
		{
			private readonly KeyCode keyCode;

			public Button(KeyCode keyCode)
			{
				this.keyCode = keyCode;
			}

			public bool GetButton() => Input.GetKey(keyCode);
			public bool GetButtonDown() => Input.GetKeyDown(keyCode);
			public bool GetButtonUp() => Input.GetKeyUp(keyCode);
		}
	}
}