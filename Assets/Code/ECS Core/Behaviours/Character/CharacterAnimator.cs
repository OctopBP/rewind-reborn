using Rewind.ECSCore.Enums;
using TMPro;
using UnityEngine;

namespace Rewind.ECSCore {
	public class CharacterAnimator : MonoBehaviour,
		IAnyClockStateListener, ICharacterLookDirectionListener, ICharacterStateListener {
		[SerializeField] TMP_Text clockStateText;
		[SerializeField] TMP_Text characterLookDirectionText;
		[SerializeField] TMP_Text characterStateText;
		
		public void OnAnyClockState(GameEntity _, ClockState value) {
			clockStateText.text = $"ClockState: {value}";
			Debug.Log($"ClockState: {value}");
		}

		public void OnCharacterLookDirection(GameEntity _, CharacterLookDirection value) {
			characterLookDirectionText.text = $"CharacterLookDirection: {value}";
			Debug.Log($"CharacterLookDirection: {value}");
		}

		public void OnCharacterState(GameEntity _, CharacterState value) {
			characterStateText.text = $"CharacterState: {value}";
			Debug.Log($"CharacterState: {value}");
		}
	}
}