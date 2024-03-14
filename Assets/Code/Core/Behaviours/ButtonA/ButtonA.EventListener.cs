using Rewind.SharedData;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class ButtonA : IButtonAStateListener, IHoldedAtTimeListener, IHoldedAtTimeRemovedListener
	{
		[Header("Status indication")]
		[SerializeField] private TMP_Text statusText;
		[SerializeField] private TMP_Text holdText;

		[SerializeField] private StatusIndicator closedStatus;
		[SerializeField] private StatusIndicator openedStatus;

		[Header("PressAnimation")]
		[SerializeField] private Transform buttonTransform;
		[SerializeField] private float openPosition;
		[SerializeField] private float closePosition;

		public void OnButtonAState(GameEntity _, ButtonAState value)
		{
			buttonTransform.localPosition = Vector3.up * (value == ButtonAState.Closed ? closePosition : openPosition);

			var status = value == ButtonAState.Closed ? closedStatus : openedStatus;
			statusText.SetText(status.text);
			statusText.color = status.color;
		}

		public void OnHoldedAtTime(GameEntity _, float value) => holdText.gameObject.SetActive(true);
		public void OnHoldedAtTimeRemoved(GameEntity _) => holdText.gameObject.SetActive(false);
	}
}