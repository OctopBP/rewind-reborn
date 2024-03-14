using System;
using Code.Helpers.Tracker;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity.Editor;
using UnityEngine;

namespace Rewind.Infrastructure
{
	public interface ILink
	{
		void Unlink();
	}

	public class EntityModel<T> where T : IEntity
	{
		public readonly T entity;

		protected EntityModel()
		{
			IContext context = typeof(T) switch
			{
				{ } t when t == typeof(GameEntity) => Contexts.sharedInstance.game,
				{ } t when t == typeof(InputEntity) => Contexts.sharedInstance.input,
				{ } t when t == typeof(ConfigEntity) => Contexts.sharedInstance.config,
				_ => throw new ArgumentOutOfRangeException($"Can\'t find context for {typeof(T)} type")
			};

			entity = (T) context.CreateEntity();
		}
	}
	
	public class TrackedEntityModel<T> : EntityModel<T> where T : IEntity 
	{
		protected TrackedEntityModel(ITracker tracker)
		{
			tracker.Track(() => entity.Destroy());
		}
	}
	
	public class LinkedEntityModel<T> : TrackedEntityModel<T>, ILink where T : IEntity
	{
		private readonly GameObjectLink gameObjectLink;

		protected LinkedEntityModel(GameObject gameObject, ITracker tracker) : base(tracker)
		{
			gameObjectLink = new GameObjectLink(gameObject, entity);
		}

		public void Unlink() => gameObjectLink.Unlink();
	}

	public abstract class EntityLinkBehaviour<TModel> : MonoBehaviour where TModel : ILink
	{
		protected TModel model;
		protected IDisposableTracker tracker;
		
		public void Initialize()
		{
			model = CreateModel();
			tracker = new DisposableTracker();
			tracker.Track(() => model.Unlink());
		}
		protected abstract TModel CreateModel();
		private void OnDestroy() => tracker.Dispose();
	}
	
	public abstract class EntityLinkBehaviour<TModel, TParam> : MonoBehaviour where TModel : ILink
	{
		private TModel model;

		public void Initialize(TParam param) => model = CreateModel(param);
		protected abstract TModel CreateModel(TParam p);
		private void OnDestroy() => model?.Unlink();
	}

	public class GameObjectLink
	{
		private readonly GameObject gameObject;

		public GameObjectLink(GameObject gameObject, IEntity entity)
		{
			this.gameObject = gameObject;
			gameObject.Link(entity);
		}

		public void Unlink() => gameObject.Unlink();
	}
}