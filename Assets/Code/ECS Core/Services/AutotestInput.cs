using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Services
{
	[CreateAssetMenu(fileName = "Autotest Input", menuName = "Autotest/Input Actions", order = 0)]
	[InlineEditor]
	public class AutotestInput : ScriptableObject
	{
		[TableList] public List<InputAction> actions = new();

		[Serializable]
		public class InputAction
		{
			public enum ButtonStatus { None, Active, Done }

			[ReadOnly] public ButtonStatus status;
			public float downTime;
			public float upTime;
			public KeyCode code;

			public InputAction(float downTime, float upTime, KeyCode code)
			{
				status = ButtonStatus.None;
				this.downTime = downTime;
				this.upTime = upTime;
				this.code = code;
			}
		}
	}

	public static class ButtonStatusExt
	{
		public static bool IsNone(this AutotestInput.InputAction.ButtonStatus status) =>
			status == AutotestInput.InputAction.ButtonStatus.None;

		public static bool IsActive(this AutotestInput.InputAction.ButtonStatus status) =>
			status == AutotestInput.InputAction.ButtonStatus.Active;

		public static bool IsDone(this AutotestInput.InputAction.ButtonStatus status) =>
			status == AutotestInput.InputAction.ButtonStatus.Done;
	}
}