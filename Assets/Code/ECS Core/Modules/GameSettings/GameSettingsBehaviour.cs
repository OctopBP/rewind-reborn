using Rewind.SharedData;
using UnityEngine;

namespace Rewind.ECSCore {
	public class GameSettingsBehaviour : MonoBehaviour {
		[SerializeField] public GameSettingsData gameSettingsData;

		public void initialize() {
			var context = Contexts.sharedInstance.config;
			context.SetGameSettings(gameSettingsData);
		}
	}
}