using Rewind.Data;
using Rewind.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rewind.ECSCore {
	public class GameSettingsBehaviour : ComponentBehaviour {
		// todo: remove FormerlySerializedAs
		[FormerlySerializedAs("gameSettings")] [SerializeField] GameSettingsData gameSettingsData;

		protected override void onAwake() {
			entity.AddGameSettings(gameSettingsData);
		}
	}
}