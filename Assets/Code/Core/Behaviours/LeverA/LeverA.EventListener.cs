using Rewind.SharedData;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours
{
	public partial class LeverA : ILeverAStateListener, IHoldedAtTimeListener, IHoldedAtTimeRemovedListener
	{
		[Header("Status indication")]
		[SerializeField] private TMP_Text statusText;
		[SerializeField] private TMP_Text holdText;

		[SerializeField] private StatusIndicator closedStatus;
		[SerializeField] private StatusIndicator openedStatus;

		[Header("PressAnimation")]
		[SerializeField] private Transform leverTransform;
		[SerializeField] private float openAngle;
		[SerializeField] private float closeAngle;

		public void OnLeverAState(GameEntity _, LeverAState value) => SetState(value);

		private void SetState(LeverAState value)
		{
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