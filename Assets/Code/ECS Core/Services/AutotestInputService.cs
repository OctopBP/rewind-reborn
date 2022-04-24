using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rewind.Services {
	public class AutotestInputService : MonoBehaviour, IInputService {
		enum ButtonState { Pressed, Opened, Down, Up };

		enum ButtonPress { Down, Up };

		class Button {
			ButtonState state { get; set; }

			public Button() {
				state = ButtonState.Opened;
			}

			public void update(ButtonPress pressStatus) {
				state = pressStatus switch {
					ButtonPress.Up => ButtonState.Up,
					ButtonPress.Down => ButtonState.Down,
					_ => throw new ArgumentOutOfRangeException(nameof(pressStatus), pressStatus, null)
				};
			}

			public void tick() {
				state = state switch {
					ButtonState.Down => ButtonState.Pressed,
					ButtonState.Pressed => ButtonState.Pressed,
					ButtonState.Opened => ButtonState.Opened,
					ButtonState.Up => ButtonState.Opened,
					_ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
				};
			}

			public bool getButton() => state == ButtonState.Pressed;
			public bool getButtonDown() => state == ButtonState.Down;
			public bool getButtonUp() => state == ButtonState.Up;
		}

		[SerializeField] AutotestInput autotestInput;

		readonly Button rightButton = new();
		readonly Button leftButton = new();
		readonly Button upButton = new();
		readonly Button downButton = new();
		readonly Button interactButton = new();
		readonly Button interactSecondButton = new();
		readonly Button rewindButton = new();

		static Init init;

		void Start() => init = new(
			autotestInput, rightButton, leftButton, upButton, downButton, interactButton, interactSecondButton, rewindButton
		);
		void Update() => init.update();

		class Init {
			readonly AutotestInput autotestInput;

			readonly Button rightButton;
			readonly Button leftButton;
			readonly Button upButton;
			readonly Button downButton;
			readonly Button interactButton;
			readonly Button interactSecondButton;
			readonly Button rewindButton;

			List<Button> buttons => new() {
				rightButton,
				leftButton,
				upButton,
				downButton,
				interactButton,
				interactSecondButton,
				rewindButton
			};

			public Init(
				AutotestInput autotestInput, Button rightButton, Button leftButton, Button upButton,
				Button downButton, Button interactButton, Button interactSecondButton, Button rewindButton
			) {
				foreach (var action in autotestInput.actions) {
					action.status = AutotestInput.InputAction.ButtonStatus.None;
				}

				this.autotestInput = autotestInput;
				this.rightButton = rightButton;
				this.leftButton = leftButton;
				this.upButton = upButton;
				this.downButton = downButton;
				this.interactButton = interactButton;
				this.interactSecondButton = interactSecondButton;
				this.rewindButton = rewindButton;
			}

			Button button(KeyCode code) =>
				code switch {
					KeyCode.D => rightButton,
					KeyCode.A => leftButton,
					KeyCode.W => upButton,
					KeyCode.S => downButton,
					KeyCode.E => interactButton,
					KeyCode.Q => interactSecondButton,
					KeyCode.T => rewindButton,
					_ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
				};

			public void update() {
				foreach (var button in buttons) {
					button.tick();
				}

				var currentDownActions = autotestInput.actions.Where(a => a.status.isNone() && a.downTime <= Time.time);
				foreach (var action in currentDownActions.ToList()) {
					button(action.code).update(ButtonPress.Down);
					action.status = AutotestInput.InputAction.ButtonStatus.Active;
				}

				var currentUpActions = autotestInput.actions.Where(a => a.status.isActive() && a.upTime <= Time.time);
				foreach (var action in currentUpActions.ToList()) {
					button(action.code).update(ButtonPress.Up);
					action.status = AutotestInput.InputAction.ButtonStatus.Done;
				}
			}
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

		public bool getInteractSecondButton() => interactSecondButton.getButton();
		public bool getInteractSecondButtonDown() => interactSecondButton.getButtonDown();
		public bool getInteractSecondButtonUp() => interactSecondButton.getButtonUp();

		public bool getRewindButton() => rewindButton.getButton();
		public bool getRewindButtonDown() => rewindButton.getButtonDown();
		public bool getRewindButtonUp() => rewindButton.getButtonUp();
	}
}