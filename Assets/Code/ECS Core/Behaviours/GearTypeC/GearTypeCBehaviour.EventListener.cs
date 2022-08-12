using System;
using Code.Base;
using Entitas;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Rewind.Infrastructure;
using Rewind.Services;
using Rewind.ViewListeners;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeCBehaviour : IEventListener, IGearTypeCStateListener,
		IHoldedAtTimeListener, IHoldedAtTimeRemovedListener, IAwake
	{
		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator rotationRightStatus;
		[SerializeField] StatusIndicator rotationLeftStatus;

		public void Awake() {
			OnGearTypeCState(null, GearTypeCState.Closed);
			OnHoldedAtTimeRemoved(null);
		}

		public void registerListeners() {
			entity.AddGearTypeCStateListener(this);
			entity.AddHoldedAtTimeListener(this);
			entity.AddHoldedAtTimeRemovedListener(this);
		}

		public void unregisterListeners() {
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