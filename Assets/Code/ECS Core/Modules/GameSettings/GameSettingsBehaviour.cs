using Rewind.Data;
using Rewind.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rewind.ECSCore {
	public class GameSettingsBehaviour : MonoBehaviour {
		// todo: remove FormerlySerializedAs
		[FormerlySerializedAs("gameSettings")] [SerializeField] GameSettingsData gameSettingsData;

		void Start() {
			var context = Contexts.sharedInstance.game;
			var entity = context.CreateEntity();
			entity.AddGameSettings(gameSettingsData);
		}
	}
}