using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Services {
	public class AutotestInputService : MonoBehaviour, IInputService {
		enum ButtonState { Pressed, Opened, Down, Up };

		public enum ButtonPress { Down, Up };

		[Serializable]
		public class InputAction {
			[ReadOnly] public bool done;
			public float time;
			public KeyCode code;
			public ButtonPress pressStatus;
			
			public InputAction(float time, KeyCode code, ButtonPress pressStatus) {
				done = false;
				this.time = time;
				this.code = code;
				this.pressStatus = pressStatus;
			}
		}

		public class Button {
			ButtonState state { get; set; }

			public Button() {
				state = ButtonState.Opened;
			}

			public void update(ButtonPress pressStatus) {
				state = pressStatus switch {
					ButtonPress.Up => ButtonState.Up,
					ButtonPress.Down => ButtonState.Down
				};
			}

			public void tick() {
				state = state switch {
					ButtonState.Down => ButtonState.Pressed,
					ButtonState.Pressed => ButtonState.Pressed,
					ButtonState.Opened => ButtonState.Opened,
					ButtonState.Up => ButtonState.Opened,
				};
			}

			public bool getButton() => state == ButtonState.Pressed;
			public bool getButtonDown() => state == ButtonState.Down;
			public bool getButtonUp() => state == ButtonState.Up;
		}

		[SerializeField, TableList] List<InputAction> actions = new() {
			new(1.0f, KeyCode.D, ButtonPress.Down),
			new(2.0f, KeyCode.D, ButtonPress.Up),
			new(3.0f, KeyCode.E, ButtonPress.Down),
			new(5.0f, KeyCode.E, ButtonPress.Up),
			new(6.0f, KeyCode.T, ButtonPress.Down),
			new(6.2f, KeyCode.T, ButtonPress.Up)
		};

		readonly Button rightButton = new();
		readonly Button leftButton = new();
		readonly Button interactButton = new();
		readonly Button rewindButton = new();

		public static Init init;

		void Start() {
			init = new(actions, rightButton, leftButton, interactButton, rewindButton);
		}

		public class Init {
			readonly List<InputAction> actions;

			readonly Button rightButton;
			readonly Button leftButton;
			readonly Button interactButton;
			readonly Button rewindButton;
			
			List<Button> buttons => new() {
				rightButton,
				leftButton,
				interactButton,
				rewindButton
			};

			public Init(
				List<InputAction> actions, Button rightButton, Button leftButton,
				Button interactButton, Button rewindButton
			) {
				this.actions = actions;
				this.rightButton = rightButton;
				this.leftButton = leftButton;
				this.interactButton = interactButton;
				this.rewindButton = rewindButton;
			}

			Button button(KeyCode code) =>
				code switch {
					KeyCode.D => rightButton,
					KeyCode.A => leftButton,
					KeyCode.E => interactButton,
					KeyCode.T => rewindButton
				};

			
		public void update() {
				foreach (var button in buttons) {
					button.tick();
				}

				var currentActions = actions.Where(a => !a.done && a.time <= Time.time);
				foreach (var action in currentActions.ToList()) {
					button(action.code).update(action.pressStatus);
					action.done = true;
				}
			}
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
	}
}