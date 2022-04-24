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
	public class GearTypeCBehaviour : SelfInitializedViewWithId, IEventListener,
		IGearTypeCStateListener, IHoldedAtTimeListener, IHoldedAtTimeRemovedListener, IStatusValue
	{
		[SerializeField] GearTypeCData data;
		[SerializeField] PathPointType pointIndex;

		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator rotationRightStatus;
		[SerializeField] StatusIndicator rotationLeftStatus;

		public PathPointType point => pointIndex;

		public float statusValue => entity.gearTypeCState.value switch {
			GearTypeCState.Closed => 0,
			GearTypeCState.RotationRight => 1,
			GearTypeCState.RotationLeft => -1,
			_ => throw new ArgumentOutOfRangeException()
		};

		protected override void onAwake() {
			base.onAwake();
			setupGear();

			OnGearTypeCState(null, GearTypeCState.Closed);
			OnHoldedAtTimeRemoved(null);
		}

		void setupGear() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isGearTypeC = true);
			entity.with(x => x.isPuzzleElement = true);

			entity.AddGearTypeCData(data);
			entity.AddGearTypeCState(GearTypeCState.Closed);

			entity.AddPointIndex(pointIndex);
			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);
		}

		public void registerListeners(IEntity _) {
			entity.AddGearTypeCStateListener(this);
			entity.AddHoldedAtTimeListener(this);
			entity.AddHoldedAtTimeRemovedListener(this);
		}

		public void unregisterListeners(IEntity _) {
			entity.RemoveGearTypeCStateListener(this);
			entity.RemoveHoldedAtTimeListener(this);
			entity.RemoveHoldedAtTimeRemovedListener(this);
		}

		public void OnGearTypeCState(GameEntity _, GearTypeCState value) {
			var status = value switch {
				GearTypeCState.Closed => closedStatus,
				GearTypeCState.RotationRight => rotationRightStatus,
				GearTypeCState.RotationLeft => rotationLeftStatus,
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