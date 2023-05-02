using System;
using Rewind.ECSCore.Enums;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeA : IGearTypeAStateListener, IHoldedAtTimeListener, IHoldedAtTimeRemovedListener {
		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator closingStatus;
		[SerializeField] StatusIndicator openingStatus;
		[SerializeField] StatusIndicator openedStatus;

		public void OnGearTypeAState(GameEntity _, GearTypeAState value) => setStatus(value);
		public void OnHoldedAtTime(GameEntity _, float value) => setHoldedMarker(true);
		public void OnHoldedAtTimeRemoved(GameEntity _) => setHoldedMarker(false);

		void setStatus(GearTypeAState value) {
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

		void setHoldedMarker(bool becomeHolded) => 	holdText.gameObject.SetActive(becomeHolded);
	}
}