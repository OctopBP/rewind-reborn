using UnityEngine;

namespace Rewind.Data {
	[CreateAssetMenu(fileName = "GameSettings", menuName = "Data/Game Settings", order = 1)]
	public class GameSettingsData : ScriptableObject {
		public float rewindTime;
	}
}