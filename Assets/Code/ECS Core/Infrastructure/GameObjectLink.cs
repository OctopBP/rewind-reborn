using System;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity.Editor;
using UnityEngine;

namespace Rewind.Infrastructure {
	public interface ILink {
		void unlink();
	}

	public class EntityModel<T> where T : IEntity {
		public readonly T entity;

		protected EntityModel() {
			IContext context = typeof(T) switch {
				Type t when t == typeof(GameEntity) => Contexts.sharedInstance.game,
				Type t when t == typeof(InputEntity) => Contexts.sharedInstance.input,
				Type t when t == typeof(ConfigEntity) => Contexts.sharedInstance.config,
				_ => throw new ArgumentOutOfRangeException($"Can\'t find context for {typeof(T)} type")
			};

			entity = (T) context.CreateEntity();
		}
	}
	
	public class LinkedEntityModel<T> : EntityModel<T>, ILink where T : IEntity {
		readonly GameObjectLink gameObjectLink;

		protected LinkedEntityModel(GameObject gameObject) {
			gameObjectLink = new GameObjectLink(gameObject, entity);
		}

		public void unlink() => gameObjectLink.unlink();
	}

	public abstract class EntityLinkBehaviour<TModel> : MonoBehaviour where TModel : ILink {
		protected TModel model;

		public void initialize() => model = createModel();
		protected abstract TModel createModel();
		void OnDestroy() => model?.unlink();
	}
	
	public abstract class EntityLinkBehaviour<TModel, TParam> : MonoBehaviour where TModel : ILink {
		TModel model;

		public void initialize(TParam param) => model = createModel(param);
		protected abstract TModel createModel(TParam p);
		void OnDestroy() => model?.unlink();
	}

	public class GameObjectLink {
		readonly GameObject gameObject;

		public GameObjectLink(GameObject gameObject, IEntity entity) {
			this.gameObject = gameObject;
			gameObject.Link(entity);
		}

		public void unlink() => gameObject.Unlink();
	}
}