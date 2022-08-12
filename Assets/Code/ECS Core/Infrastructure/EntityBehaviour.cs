using Rewind.Helpers.Interfaces.UnityCallbacks;
using UnityEngine;

namespace Rewind.Infrastructure {
	public class EntityBehaviour : MonoBehaviour, IAwake {
		[SerializeReference] ComponentBehaviour[] componentBehaviours;

		public static GameContext gameContext => Contexts.sharedInstance.game;
		public GameEntity entity { get; private set; }

		public void Awake() {
			entity = gameContext.CreateEntity();

			foreach (var componentBehaviour in componentBehaviours) {
				componentBehaviour.initialize(entity, gameContext);
			}
		}
	}
}