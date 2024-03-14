using ExhaustiveMatching;
using Rewind.SharedData;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours
{
	public partial class GearTypeA : IGearTypeAStateListener, IHoldedAtTimeListener, IHoldedAtTimeRemovedListener
    {
		[Header("Status indication")]
		[SerializeField] private TMP_Text statusText;
		[SerializeField] private TMP_Text holdText;

		[SerializeField] private StatusIndicator closedStatus;
		[SerializeField] private StatusIndicator closingStatus;
		[SerializeField] private StatusIndicator openingStatus;
		[SerializeField] private StatusIndicator openedStatus;

		public void OnGearTypeAState(GameEntity _, GearTypeAState value) => SetStatus(value);
		public void OnHoldedAtTime(GameEntity _, float value) => SetHoldedMarker(true);
		public void OnHoldedAtTimeRemoved(GameEntity _) => SetHoldedMarker(false);

		private void SetStatus(GearTypeAState value)
        {
			var status = value switch {
				GearTypeAState.Closed => closedStatus,
				GearTypeAState.Closing => closingStatus,
				GearTypeAState.Opened => openedStatus,
				GearTypeAState.Opening => openingStatus,
				_ => throw ExhaustiveMatch.Failed(value)
			};

			statusText.SetText(status.text);
			statusText.color = status.color;
		}

		private void SetHoldedMarker(bool becomeHolded) => 	holdText.gameObject.SetActive(becomeHolded);
	}
}