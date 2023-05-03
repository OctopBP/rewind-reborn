using Rewind.SharedData;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class LeverA : ILeverAStateListener, IHoldedAtTimeListener, IHoldedAtTimeRemovedListener {
		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator openedStatus;

		[Header("PressAnimation")]
		[SerializeField] Transform leverTransform;
		[SerializeField] float openAngle;
		[SerializeField] float closeAngle;

		public void OnLeverAState(GameEntity _, LeverAState value) => setState(value);

		void setState(LeverAState value) {
			var isClosed = value == LeverAState.Closed;
			leverTransform.localRotation = Quaternion.AngleAxis(isClosed ? closeAngle : openAngle, Vector3.forward);

			var status = isClosed ? closedStatus : openedStatus;
			statusText.SetText(status.text);
			statusText.color = status.color;
		}

		public void OnHoldedAtTime(GameEntity _, float value) =>
			holdText.gameObject.SetActive(true);

		public void OnHoldedAtTimeRemoved(GameEntity _) =>
			holdText.gameObject.SetActive(false);
	}
}