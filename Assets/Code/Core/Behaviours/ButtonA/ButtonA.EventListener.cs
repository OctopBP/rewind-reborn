using Rewind.SharedData;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class ButtonA : IButtonAStateListener, IHoldedAtTimeListener, IHoldedAtTimeRemovedListener {
		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator openedStatus;

		[Header("PressAnimation")]
		[SerializeField] Transform buttonTransform;
		[SerializeField] float openPosition;
		[SerializeField] float closePosition;

		public void OnButtonAState(GameEntity _, ButtonAState value) {
			buttonTransform.localPosition = Vector3.up * (value == ButtonAState.Closed ? closePosition : openPosition);

			var status = value == ButtonAState.Closed ? closedStatus : openedStatus;
			statusText.SetText(status.text);
			statusText.color = status.color;
		}

		public void OnHoldedAtTime(GameEntity _, float value) => holdText.gameObject.SetActive(true);
		public void OnHoldedAtTimeRemoved(GameEntity _) => holdText.gameObject.SetActive(false);
	}
}