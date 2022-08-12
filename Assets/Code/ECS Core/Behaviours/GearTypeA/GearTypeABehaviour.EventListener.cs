using System;
using Code.Base;
using Entitas;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Helpers.Interfaces.UnityCallbacks;
using Rewind.Services;
using Rewind.ViewListeners;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeABehaviour : IEventListener, IGearTypeAStateListener,
		IHoldedAtTimeListener, IHoldedAtTimeRemovedListener, IAwake
	{
		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator closingStatus;
		[SerializeField] StatusIndicator openingStatus;
		[SerializeField] StatusIndicator openedStatus;

		public void Awake() {
			OnGearTypeAState(null, GearTypeAState.Closed);
			OnHoldedAtTimeRemoved(null);
		}

		public void registerListeners() {
			entity.AddGearTypeAStateListener(this);
			entity.AddHoldedAtTimeListener(this);
			entity.AddHoldedAtTimeRemovedListener(this);
		}

		public void unregisterListeners() {
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