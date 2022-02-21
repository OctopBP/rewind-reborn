using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.VisualDebugging.Unity;
using Rewind.Services;
using UnityEngine;
using EntityBehaviour = Rewind.Infrastructure.EntityBehaviour;

namespace Rewind.ViewListeners {
	public class UnityViewController : MonoBehaviour, IViewController {
		public GameEntity entity { get; private set; }

		public IViewController initializeView(GameContext game, IEntity entity) {
			this.entity = (GameEntity) entity;

			setupView();
			registerViewComponents();

			return this;
		}

		public void destroy() {
			gameObject.unregisterListeners(entity);
			gameObject.DestroyGameObject();
		}

		void setupView() {
			entity.AddViewController(this);
			entity.AddView(gameObject);
		}

		void registerViewComponents() {
			inflateEntityBehaviours();
		}

		void inflateEntityBehaviours() {
			foreach (var behaviour in fittingBehaviours()) {
				behaviour.viewController ??= this;
			}

			IEnumerable<EntityBehaviour> fittingBehaviours() =>
				GetComponentsInChildren<EntityBehaviour>().Where(x => x.gameObject == gameObject);
		}
	}

	public static partial class CleanCodeExtensions {
		public static void unregisterListeners(this GameObject view, IEntity with) {
			foreach (var listener in view.GetComponents<IEventListener>())
				listener.unregisterListeners(with);
		}
	}
}