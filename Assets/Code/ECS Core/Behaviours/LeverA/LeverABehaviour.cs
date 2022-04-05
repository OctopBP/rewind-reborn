using System;
using Code.Base;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public class LeverABehaviour : SelfInitializedViewWithId, IEventListener, ILeverAStateListener,
		IHoldedAtTimeListener, IHoldedAtTimeRemovedListener, IStatusValue
	{
		[SerializeField] PathPointType pointIndex;

		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator openedStatus;

		[Header("PressAnimation")]
		[SerializeField] Transform leverTransform;
		[SerializeField] float openAngle;
		[SerializeField] float closeAngle;

		public PathPointType getPointIndex => pointIndex;

		public float statusValue => entity.leverAState.value switch {
			LeverAState.Closed => 0,
			LeverAState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};

		protected override void onAwake() {
			base.onAwake();
			setupLever();

			OnLeverAState(entity, LeverAState.Closed);
		}

		void setupLever() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isLeverA = true);
			entity.with(x => x.isPuzzleElement = true);
			
			entity.AddLeverAState(LeverAState.Closed);

			entity.AddPointIndex(pointIndex);
			entity.AddPosition(transform.position);
		}

		public void registerListeners(IEntity _) {
			entity.AddLeverAStateListener(this);
			entity.AddHoldedAtTimeListener(this);
			entity.AddHoldedAtTimeRemovedListener(this);
		}

		public void unregisterListeners(IEntity _) {
			entity.RemoveLeverAStateListener(this);
			entity.RemoveHoldedAtTimeListener(this);
			entity.RemoveHoldedAtTimeRemovedListener(this);
		}

		public void OnLeverAState(GameEntity _, LeverAState value) {
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