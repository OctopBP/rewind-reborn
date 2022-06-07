using System.Linq;
using UnityEngine;

namespace Rewind.Services.Autotest {
	public partial class AutotestInputService : MonoBehaviour, IInputService {
		[SerializeField] AutotestInput autotestInput;

		readonly InputKeyCode[] keyCodeMap = {
			new(InputType.Right, KeyCode.D),
			new(InputType.Left, KeyCode.A),
			new(InputType.Up, KeyCode.W),
			new(InputType.Down, KeyCode.S),
			new(InputType.Interact, KeyCode.E),
			new(InputType.InteractSecond, KeyCode.Q),
			new(InputType.Rewind, KeyCode.T)
		};

		void Start() {
			foreach (var action in autotestInput.actions) {
				action.status = AutotestInput.InputAction.ButtonStatus.None;
			}
		}

		void Update() {
			foreach (var inputKeyCode in keyCodeMap) {
				inputKeyCode.button.tick();
			}

			var currentDownActions = autotestInput.actions.Where(a => a.status.isNone() && a.downTime <= Time.time);
			foreach (var action in currentDownActions.ToList()) {
				setStatus(action.code, ButtonPress.Down);
				action.status = AutotestInput.InputAction.ButtonStatus.Active;
			}

			var currentUpActions = autotestInput.actions.Where(a => a.status.isActive() && a.upTime <= Time.time);
			foreach (var action in currentUpActions.ToList()) {
				setStatus(action.code, ButtonPress.Up);
				action.status = AutotestInput.InputAction.ButtonStatus.Done;
			}

			void setStatus(KeyCode keyCode, ButtonPress status) {
				foreach (var kvp in keyCodeMap.Where(value => value.keyCode == keyCode)) {
					kvp.button.update(status);	
				}
			}	
		}

		bool getButtonAtType(InputType type) => keyCodeMap.Where(i => i.type == type).Any(i => i.button.getButton());
		bool getButtonDownAtType(InputType type) => keyCodeMap.Where(i => i.type == type).Any(i => i.button.getButtonDown());
		bool getButtonUpAtType(InputType type) => keyCodeMap.Where(i => i.type == type).Any(i => i.button.getButtonUp());

		public bool getMoveRightButton() => getButtonAtType(InputType.Right);
		public bool getMoveRightButtonDown() => getButtonDownAtType(InputType.Right);
		public bool getMoveRightButtonUp() => getButtonUpAtType(InputType.Right);

		public bool getMoveLeftButton() => getButtonAtType(InputType.Left);
		public bool getMoveLeftButtonDown() => getButtonDownAtType(InputType.Left);
		public bool getMoveLeftButtonUp() => getButtonUpAtType(InputType.Left);

		public bool getMoveUpButton() => getButtonAtType(InputType.Up);
		public bool getMoveUpButtonDown() => getButtonDownAtType(InputType.Up);
		public bool getMoveUpButtonUp() => getButtonUpAtType(InputType.Up);

		public bool getMoveDownButton() => getButtonAtType(InputType.Down);
		public bool getMoveDownButtonDown() => getButtonDownAtType(InputType.Down);
		public bool getMoveDownButtonUp() => getButtonUpAtType(InputType.Down);

		public bool getInteractButton() => getButtonAtType(InputType.Interact);
		public bool getInteractButtonDown() => getButtonDownAtType(InputType.Interact);
		public bool getInteractButtonUp() => getButtonUpAtType(InputType.Interact);

		public bool getInteractSecondButton() => getButtonAtType(InputType.InteractSecond);
		public bool getInteractSecondButtonDown() => getButtonDownAtType(InputType.InteractSecond);
		public bool getInteractSecondButtonUp() => getButtonUpAtType(InputType.InteractSecond);

		public bool getRewindButton() => getButtonAtType(InputType.Rewind);
		public bool getRewindButtonDown() => getButtonDownAtType(InputType.Rewind);
		public bool getRewindButtonUp() => getButtonUpAtType(InputType.Rewind);
	}
}