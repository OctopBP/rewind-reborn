using UnityEngine;

namespace Rewind.SharedData {
	[CreateAssetMenu(fileName = "GameSettings", menuName = "Data/Game Settings", order = 1)]
	public partial class GameSettingsData : ScriptableObject {
		[SerializeField, PublicAccessor] float rewindTime;
		[SerializeField, PublicAccessor] float clockNormalSpeed = 1;
		[SerializeField, PublicAccessor] float clockRewindSpeed = 1;

		[Space(10)]
		[SerializeField, PublicAccessor] float characterSpeed = 1;
	}
}