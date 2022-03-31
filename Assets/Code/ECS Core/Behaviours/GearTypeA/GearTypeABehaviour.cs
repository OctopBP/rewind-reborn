using System;
using Code.Base;
using Entitas;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public class GearTypeABehaviour : SelfInitializedViewWithId, IEventListener,
		IGearTypeAStateListener, IHoldedAtTimeListener, IHoldedAtTimeRemovedListener, IStatusValue
	{
		[SerializeField] GearTypeAData data;
		[SerializeField] PathPointType pointIndex;

		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator closingStatus;
		[SerializeField] StatusIndicator openingStatus;
		[SerializeField] StatusIndicator openedStatus;

		public PathPointType point => pointIndex;

		public float statusValue => entity.gearTypeAState.value switch {
			GearTypeAState.Closed => 0,
			GearTypeAState.Closing => 0.4f,
			GearTypeAState.Opening => 0.6f,
			GearTypeAState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};

		protected override void onAwake() {
			base.onAwake();
			setupGear();

			OnGearTypeAState(null, GearTypeAState.Closed);
			OnHoldedAtTimeRemoved(null);
		}

		void setupGear() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isGearTypeA = true);

			entity.AddGearTypeAData(data);
			entity.AddGearTypeAState(GearTypeAState.Closed);

			entity.AddPointIndex(pointIndex);
			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);
		}

		public void registerListeners(IEntity _) {
			entity.AddGearTypeAStateListener(this);
			entity.AddHoldedAtTimeListener(this);
			entity.AddHoldedAtTimeRemovedListener(this);
		}

		public void unregisterListeners(IEntity _) {
			entity.RemoveGearTypeAStateListener(this);
			entity.RemoveHoldedAtTimeListener(this);
			entity.RemoveHoldedAtTimeRemovedListener(this);
		}

		public void OnGearTypeAState(GameEntity _, GearTypeAState value) {
			var status = value switch {
				GearTypeAState.Closed => closedStatus,
				GearTypeAState.Closing => closingStatus,
				GearTypeAState.Opened => openedStatus,
				GearTypeAState.Opening => openingStatus,
				_ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
			};

			statusText.SetText(status.text);
			statusText.color = status.color;
		}

		public void OnHoldedAtTime(GameEntity _, float value) =>
			holdText.gameObject.SetActive(true);

		public void OnHoldedAtTimeRemoved(GameEntity _) =>
			holdText.gameObject.SetActive(false);
	}
}