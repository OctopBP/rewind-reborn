using System;
using Rewind.ECSCore.Enums;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeC : IGearTypeCStateListener, IHoldedAtTimeListener,
		IHoldedAtTimeRemovedListener
	{
		[Header("Status indication")]
		[SerializeField] TMP_Text statusText;
		[SerializeField] TMP_Text holdText;

		[SerializeField] StatusIndicator closedStatus;
		[SerializeField] StatusIndicator rotationRightStatus;
		[SerializeField] StatusIndicator rotationLeftStatus;

		public void OnGearTypeCState(GameEntity _, GearTypeCState value) => onNewState(value);
		public void OnHoldedAtTime(GameEntity _, float value) => setHoldMarker(true);
		public void OnHoldedAtTimeRemoved(GameEntity _) => setHoldMarker(false);

		void onNewState(GearTypeCState value) {
			var status = value switch {
				GearTypeCState.Closed => closedStatus,
				GearTypeCState.RotationRight => rotationRightStatus,
				GearTypeCState.RotationLeft => rotationLeftStatus,
				_ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
			};

			statusText.SetText(status.text);
			statusText.color = status.color;
		}

		void setHoldMarker(bool becomeHolded) => holdText.gameObject.SetActive(becomeHolded);
	}
}