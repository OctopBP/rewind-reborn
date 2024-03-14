using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Rewind.Services
{
	public static class EntitasExtensions
	{
		public static Option<GameEntity> Filter(this GameEntity gameEntity, Func<GameEntity, bool> predicate)
		{
			return predicate(gameEntity) ? Some(gameEntity) : None;
		}

		public static Option<TEntity> First<TEntity>(this IGroup<TEntity> self) where TEntity : class, IEntity
		{
			return self.count > 0 ? Some(self.GetEntities()[0]) : None;
		}

		public static Option<TEntity> First<TEntity>(
			this IGroup<TEntity> self, Func<TEntity, bool> predicate
		) where TEntity : class, IEntity
		{
			foreach (var entity in self.GetEntities())
			{
				if (predicate(entity))
				{
					return Some(entity);
				}
			}

			return None;
		}

		public static IEnumerable<TEntity> OrderBy<TEntity, TKey>(
			this IGroup<TEntity> self, Func<TEntity, TKey> keySelector
		) where TEntity : class, IEntity
		{
			return self.GetEntities().OrderBy(keySelector);
		}

		public static IEnumerable<TEntity> OrderByDescending<TEntity, TKey>(
			this IGroup<TEntity> self, Func<TEntity, TKey> keySelector
		) where TEntity : class, IEntity
		{
			return self.GetEntities().OrderByDescending(keySelector);
		}

		public static void ForEach<TEntity>(this IGroup<TEntity> self, Action<TEntity> action)
			where TEntity : class, IEntity
		{
			foreach (var entity in self.GetEntities())
			{
				action.Invoke(entity);
			}
		}
		
		public static void ForEach<TEntity>(this IEnumerable<TEntity> self, Action<TEntity> action)
			where TEntity : class, IEntity
		{
			foreach (var entity in self)
			{
				action.Invoke(entity);
			}
		}
		
		public static IEnumerable<TEntity> Where<TEntity>(this IGroup<TEntity> self, Func<TEntity, bool> predicate)
			where TEntity : class, IEntity
		{
			return self.GetEntities().Where(predicate);
		}

		public static bool Any<TEntity>(
			this IGroup<TEntity> self, Func<TEntity, bool> predicate
		) where TEntity : class, IEntity
		{
			return self.GetEntities().Any(predicate);
		}

		public static Option<TEntity> Filter<TEntity>(this TEntity @this, Func<TEntity, bool> predicate)
			where TEntity : class, IEntity
		{
			return predicate(@this) ? Some(@this) : None;
		}
	}
}