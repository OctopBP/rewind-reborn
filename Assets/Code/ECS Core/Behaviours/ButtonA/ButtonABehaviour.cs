using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public class ButtonABehaviour : SelfInitializedViewWithId, IEventListener, IButtonAStateListener,
		IHoldedAtTimeListener, IHoldedAtTimeRemovedListener
	{
		[SerializeField] int pointIndex;
		[SerializeField] int pathIndex;

		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator openedStatus;

		[Header("PressAnimation")]
		[SerializeField] Transform buttonTransform;
		[SerializeField] float openPosition;
		[SerializeField] float closePosition;

		protected override void onAwake() {
			base.onAwake();
			setupButton();

			OnButtonAState(entity, ButtonAState.Closed);
		}

		void setupButton() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isButtonA = true);
			
			entity.AddButtonAState(ButtonAState.Closed);

			entity.AddPathIndex(pathIndex);
			entity.AddPointIndex(pointIndex);

			entity.AddPosition(transform.position);
		}

		public void registerListeners(IEntity _) {
			entity.AddButtonAStateListener(this);
			entity.AddHoldedAtTimeListener(this);
			entity.AddHoldedAtTimeRemovedListener(this);
		}

		public void unregisterListeners(IEntity _) {
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

		public void OnHoldedAtTime(GameEntity _, float value) =>
			holdText.gameObject.SetActive(true);

		public void OnHoldedAtTimeRemoved(GameEntity _) =>
			holdText.gameObject.SetActive(false);
	}
}