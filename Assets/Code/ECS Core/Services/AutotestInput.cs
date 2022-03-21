using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Services {
	[CreateAssetMenu(fileName = "Autotest Input", menuName = "Autotest/Input Actions", order = 0)]
	[InlineEditor]
	public class AutotestInput : ScriptableObject {
		[TableList] public List<InputAction> actions = new() {
			new(1.0f, 2.0f, KeyCode.D),
			new(3.0f, 5.0f, KeyCode.E),
			new(6.0f, 6.2f, KeyCode.T),
		};

		[Serializable]
		public class InputAction {
			public enum ButtonStatus { None, Active, Done }

			[ReadOnly] public ButtonStatus status;
			public float downTime;
			public float upTime;
			public KeyCode code;

			public InputAction(float downTime, float upTime, KeyCode code) {
				status = ButtonStatus.None;
				this.downTime = downTime;
				this.upTime = upTime;
				this.code = code;
			}
		}
	}

	public static class ButtonStatusExt {
		public static bool isNone(this AutotestInput.InputAction.ButtonStatus status) =>
			status == AutotestInput.InputAction.ButtonStatus.None;

		public static bool isActive(this AutotestInput.InputAction.ButtonStatus status) =>
			status == AutotestInput.InputAction.ButtonStatus.Active;

		public static bool isDone(this AutotestInput.InputAction.ButtonStatus status) =>
			status == AutotestInput.InputAction.ButtonStatus.Done;
	}
}