using ExhaustiveMatching;
using Rewind.SharedData;
using TMPro;
using UnityEngine;

namespace Rewind.Behaviours
{
	public partial class GearTypeC : IGearTypeCStateListener, IHoldedAtTimeListener,
		IHoldedAtTimeRemovedListener
	{
		[Header("Status indication")]
		[SerializeField] private TMP_Text statusText;
		[SerializeField] private TMP_Text holdText;

		[SerializeField] private StatusIndicator closedStatus;
		[SerializeField] private StatusIndicator rotationRightStatus;
		[SerializeField] private StatusIndicator rotationLeftStatus;

		public void OnGearTypeCState(GameEntity _, GearTypeCState value) => OnNewState(value);
		public void OnHoldedAtTime(GameEntity _, float value) => SetHoldMarker(true);
		public void OnHoldedAtTimeRemoved(GameEntity _) => SetHoldMarker(false);

		private void OnNewState(GearTypeCState value)
		{
			var status = value switch
			{
				GearTypeCState.Closed => closedStatus,
				GearTypeCState.RotationRight => rotationRightStatus,
				GearTypeCState.RotationLeft => rotationLeftStatus,
				_ => throw ExhaustiveMatch.Failed(value)
			};

			statusText.SetText(status.text);
			statusText.color = status.color;
		}

		private void SetHoldMarker(bool becomeHolded) => holdText.gameObject.SetActive(becomeHolded);
	}
}