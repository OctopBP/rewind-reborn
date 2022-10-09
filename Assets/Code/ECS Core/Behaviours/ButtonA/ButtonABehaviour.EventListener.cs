using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Rewind.Services;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class ButtonABehaviour : IEventListener, IButtonAStateListener,
		IHoldedAtTimeListener, IHoldedAtTimeRemovedListener, IAwake
	{
		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator openedStatus;

		[Header("PressAnimation")]
		[SerializeField] Transform buttonTransform;
		[SerializeField] float openPosition;
		[SerializeField] float closePosition;

		public void Awake() {
			OnButtonAState(entity, ButtonAState.Closed);
		}

		public void registerListeners() {
			entity.AddButtonAStateListener(this);
			entity.AddHoldedAtTimeListener(this);
			entity.AddHoldedAtTimeRemovedListener(this);
		}

		public void unregisterListeners() {
			entity.RemoveButtonAStateListener(this);
			entity.RemoveHoldedAtTimeListener(this);
			entity.RemoveHoldedAtTimeRemovedListener(this);
		}

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