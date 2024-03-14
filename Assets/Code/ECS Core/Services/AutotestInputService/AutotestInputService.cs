using System.Linq;
using UnityEngine;

namespace Rewind.Services.Autotest
{
	public partial class AutotestInputService : MonoBehaviour, IInputService
	{
		[SerializeField] private AutotestInput autotestInput;

		private readonly InputKeyCode[] keyCodeMap =
		{
			new(InputType.Right, KeyCode.D),
			new(InputType.Left, KeyCode.A),
			new(InputType.Up, KeyCode.W),
			new(InputType.Down, KeyCode.S),
			new(InputType.Interact, KeyCode.E),
			new(InputType.InteractSecond, KeyCode.Q),
			new(InputType.Rewind, KeyCode.T)
		};

		private void Start()
		{
			foreach (var action in autotestInput.actions)
			{
				action.status = AutotestInput.InputAction.ButtonStatus.None;
			}
		}

		private void Update()
		{
			foreach (var inputKeyCode in keyCodeMap)
			{
				inputKeyCode.button.Tick();
			}

			var currentDownActions = autotestInput.actions.Where(a => a.status.IsNone() && a.downTime <= Time.time);
			foreach (var action in currentDownActions.ToList())
			{
				SetStatus(action.code, ButtonPress.Down);
				action.status = AutotestInput.InputAction.ButtonStatus.Active;
			}

			var currentUpActions = autotestInput.actions.Where(a => a.status.IsActive() && a.upTime <= Time.time);
			foreach (var action in currentUpActions.ToList())
			{
				SetStatus(action.code, ButtonPress.Up);
				action.status = AutotestInput.InputAction.ButtonStatus.Done;
			}

			void SetStatus(KeyCode keyCode, ButtonPress status)
			{
				foreach (var kvp in keyCodeMap.Where(value => value.keyCode == keyCode))
				{
					kvp.button.Update(status);	
				}
			}	
		}

		private bool GetButtonAtType(InputType type) => keyCodeMap.Where(i => i.type == type).Any(i => i.button.GetButton());
		private bool GetButtonDownAtType(InputType type) => keyCodeMap.Where(i => i.type == type).Any(i => i.button.GetButtonDown());
		private bool GetButtonUpAtType(InputType type) => keyCodeMap.Where(i => i.type == type).Any(i => i.button.GetButtonUp());

		public bool GetMoveRightButton() => GetButtonAtType(InputType.Right);
		public bool GetMoveRightButtonDown() => GetButtonDownAtType(InputType.Right);
		public bool GetMoveRightButtonUp() => GetButtonUpAtType(InputType.Right);

		public bool GetMoveLeftButton() => GetButtonAtType(InputType.Left);
		public bool GetMoveLeftButtonDown() => GetButtonDownAtType(InputType.Left);
		public bool GetMoveLeftButtonUp() => GetButtonUpAtType(InputType.Left);

		public bool GetMoveUpButton() => GetButtonAtType(InputType.Up);
		public bool GetMoveUpButtonDown() => GetButtonDownAtType(InputType.Up);
		public bool GetMoveUpButtonUp() => GetButtonUpAtType(InputType.Up);

		public bool GetMoveDownButton() => GetButtonAtType(InputType.Down);
		public bool GetMoveDownButtonDown() => GetButtonDownAtType(InputType.Down);
		public bool GetMoveDownButtonUp() => GetButtonUpAtType(InputType.Down);

		public bool GetInteractButton() => GetButtonAtType(InputType.Interact);
		public bool GetInteractButtonDown() => GetButtonDownAtType(InputType.Interact);
		public bool GetInteractButtonUp() => GetButtonUpAtType(InputType.Interact);

		public bool GetInteractSecondButton() => GetButtonAtType(InputType.InteractSecond);
		public bool GetInteractSecondButtonDown() => GetButtonDownAtType(InputType.InteractSecond);
		public bool GetInteractSecondButtonUp() => GetButtonUpAtType(InputType.InteractSecond);

		public bool GetRewindButton() => GetButtonAtType(InputType.Rewind);
		public bool GetRewindButtonDown() => GetButtonDownAtType(InputType.Rewind);
		public bool GetRewindButtonUp() => GetButtonUpAtType(InputType.Rewind);
	}
}