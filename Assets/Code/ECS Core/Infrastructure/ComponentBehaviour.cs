using UnityEngine;

namespace Rewind.Infrastructure {
	public abstract class ComponentBehaviour : MonoBehaviour {
		protected GameEntity entity;
		protected GameContext gameContext;

		public void initialize(GameEntity entity, GameContext gameContext) {
			this.entity = entity;
			this.gameContext = gameContext;	
	
			initialize();
		}

		protected abstract void initialize();
	}
}