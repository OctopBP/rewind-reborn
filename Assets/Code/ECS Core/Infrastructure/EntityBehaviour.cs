using Rewind.Helpers.Interfaces.UnityCallbacks;
using UnityEngine;

namespace Rewind.Infrastructure {
	public class EntityBehaviour : MonoBehaviour, IAwake {
		[SerializeReference] ComponentBehaviour[] componentBehaviours;

		public void Awake() {
			var gameContext = Contexts.sharedInstance.game;
			var entity = gameContext.CreateEntity();

			foreach (var componentBehaviour in componentBehaviours) {
				componentBehaviour.initialize(entity, gameContext);
			}
		}
	}
}