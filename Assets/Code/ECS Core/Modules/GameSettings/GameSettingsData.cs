using UnityEngine;

namespace Rewind.Data {
	[CreateAssetMenu(fileName = "GameSettings", menuName = "Data/Game Settings", order = 1)]
	public class GameSettingsData : ScriptableObject {
		public float rewindTime;
		public float clockNormalSpeed = 1;
		public float clockRewindSpeed = 1;

		[Space(10)]
		public float characterSpeed = 1;
	}
}