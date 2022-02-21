using Rewind.Data;
using Rewind.ViewListeners;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rewind.ECSCore {
	public class GameSettingsBehaviour : SelfInitializedView {
		[FormerlySerializedAs("gameSettings")] [SerializeField] GameSettingsData gameSettingsData;

		protected override void onAwake() {
			base.onAwake();
			setupGameSettings();
		}

		void setupGameSettings() {
			entity.AddGameSettings(gameSettingsData);
		}
	}
}