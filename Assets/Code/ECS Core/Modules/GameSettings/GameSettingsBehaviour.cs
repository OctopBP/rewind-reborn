using Rewind.Data;
using Rewind.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rewind.ECSCore {
	public class GameSettingsBehaviour : ComponentBehaviour {
		// TODO: remove FormerlySerializedAs
		[FormerlySerializedAs("gameSettings")] [SerializeField] GameSettingsData gameSettingsData;

		protected override void initialize() {
			entity.AddGameSettings(gameSettingsData);
		}
	}
}