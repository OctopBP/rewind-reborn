using Rewind.ECSCore.Enums;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Rewind.Services;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class LeverABehaviour : IEventListener, ILeverAStateListener,
		IHoldedAtTimeListener, IHoldedAtTimeRemovedListener, IAwake
	{
		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator openedStatus;

		[Header("PressAnimation")]
		[SerializeField] Transform leverTransform;
		[SerializeField] float openAngle;
		[SerializeField] float closeAngle;

		public void Awake() => setState(LeverAState.Closed);

		public void registerListeners() {
			entity.AddLeverAStateListener(this);
			entity.AddHoldedAtTimeListener(this);
			entity.AddHoldedAtTimeRemovedListener(this);
		}

		public void unregisterListeners() {
			entity.RemoveLeverAStateListener(this);
			entity.RemoveHoldedAtTimeListener(this);
			entity.RemoveHoldedAtTimeRemovedListener(this);
		}

		public void OnLeverAState(GameEntity _, LeverAState value) => setState(value);

		void setState(LeverAState value) {
			leverTransform.localRotation = Quaternion.AngleAxis(
				value == LeverAState.Closed ? closeAngle : openAngle, Vector3.forward
			);

			var status = value == LeverAState.Closed ? closedStatus : openedStatus;
			statusText.SetText(status.text);
			statusText.color = status.color;
		}

		public void OnHoldedAtTime(GameEntity _, float value) =>
			holdText.gameObject.SetActive(true);

		public void OnHoldedAtTimeRemoved(GameEntity _) =>
			holdText.gameObject.SetActive(false);
	}
}