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
		readonly Button interactButton = new();
		readonly Button rewindButton = new();

		static Init init;

		void Start() {
			init = new(autotestInput, rightButton, leftButton, interactButton, rewindButton);
		}

		void Update() {
			init.update();
		}

		class Init {
			readonly AutotestInput autotestInput;

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
				AutotestInput autotestInput, Button rightButton, Button leftButton,
				Button interactButton, Button rewindButton
			) {
				this.autotestInput = autotestInput;
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
					KeyCode.T => rewindButton,
					_ => throw new ArgumentOutOfRangeException(nameof(code), code, null)
				};

			
		public void update() {
				foreach (var button in buttons) {
					button.tick();
				}

				var currentDownActions = autotestInput.actions
					.Where(a => a.status.isNone() && a.downTime <= Time.time);
				foreach (var action in currentDownActions.ToList()) {
					button(action.code).update(ButtonPress.Down);
					action.status = AutotestInput.InputAction.ButtonStatus.Active;
				}

				var currentUpActions = autotestInput.actions
					.Where(a => a.status.isActive() && a.upTime <= Time.time);
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

		public bool getInteractButton() => interactButton.getButton();
		public bool getInteractButtonDown() => interactButton.getButtonDown();
		public bool getInteractButtonUp() => interactButton.getButtonUp();

		public bool getRewindButton() => rewindButton.getButton();
		public bool getRewindButtonDown() => rewindButton.getButtonDown();
		public bool getRewindButtonUp() => rewindButton.getButtonUp();
	}
}