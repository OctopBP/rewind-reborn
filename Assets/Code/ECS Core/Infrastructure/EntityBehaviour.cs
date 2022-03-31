using Rewind.Helpers.Interfaces.UnityCallbacks;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Infrastructure {
	public class EntityBehaviour : MonoBehaviour, IAwake {
		public UnityViewController viewController;

		protected static GameContext game => Contexts.sharedInstance.game;
		public GameEntity entity => viewController.entity;

		public void Awake() {
			viewController ??= GetComponent<UnityViewController>();
			onAwake();
		}

		void Start() => onStart();
		void OnDestroy() => onDestroying();
		void OnEnable() => onEnabled();
		void OnDisable() => onDisabled();

		protected virtual void onAwake() { }
		protected virtual void onStart() { }
		protected virtual void onEnabled() { }
		protected virtual void onDisabled() { }
		protected virtual void onDestroying() { }
	}
}