using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Rewind.Services {
	public static class EntitasExtensions {
		public static Option<T> maybeValue<T>(
			this GameEntity gameEntity, Func<GameEntity, bool> predicate, Func<GameEntity, T> value
		) => predicate(gameEntity) ? Some(value(gameEntity)) : None;
		
		public static C zip<A, B, C>(this A self, B b, Func<A, B, C> f) => f(self, b);

		public static Option<TEntity> first<TEntity>(this IGroup<TEntity> self) where TEntity : class, IEntity =>
			self.count > 0 ? Some(self.GetEntities()[0]) : None;

		public static Option<TEntity> first<TEntity>(
			this IGroup<TEntity> self, Func<TEntity, bool> predicate
		) where TEntity : class, IEntity {
			foreach (var entity in self.GetEntities()) {
				if (predicate(entity))
					return Some(entity);
			}

			return None;
		}

		public static IEnumerable<TEntity> orderBy<TEntity, TKey>(
			this IGroup<TEntity> self, Func<TEntity, TKey> keySelector
		) where TEntity : class, IEntity => self.GetEntities().OrderBy(keySelector);

		public static IEnumerable<TEntity> orderByDescending<TEntity, TKey>(
			this IGroup<TEntity> self, Func<TEntity, TKey> keySelector
		) where TEntity : class, IEntity => self.GetEntities().OrderByDescending(keySelector);

		public static void forEach<TEntity>(
			this IGroup<TEntity> self, Action<TEntity> action
		) where TEntity : class, IEntity {
			foreach (var entity in self.GetEntities()) {
				action.Invoke(entity);
			}
		}
		
		public static void forEach<TEntity>(
			this IEnumerable<TEntity> self, Action<TEntity> action
		) where TEntity : class, IEntity {
			foreach (var entity in self) {
				action.Invoke(entity);
			}
		}
		
		public static IEnumerable<TEntity> where<TEntity>(this IGroup<TEntity> self, Func<TEntity, bool> predicate)
			where TEntity : class, IEntity => self.GetEntities().Where(predicate);

		public static bool any<TEntity>(
			this IGroup<TEntity> self, Func<TEntity, bool> predicate
		) where TEntity : class, IEntity => self.GetEntities().Any(predicate);

		public static Option<TEntity> filter<TEntity>(this TEntity @this, Func<TEntity, bool> predicate)
			where TEntity : class, IEntity => predicate(@this) ? Some(@this) : None;
	}
}