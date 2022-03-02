using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using LanguageExt;

namespace Rewind.Services {
	public static partial class EntitasExtensions {
		public static Option<GameEntity> first(this IGroup<GameEntity> self) =>
			self.count > 0 ? self.GetEntities()[0] : Option<GameEntity>.None;

		public static Option<GameEntity> first(
			this IGroup<GameEntity> self, Func<GameEntity, bool> predicate
		) {
			foreach (var entity in self.GetEntities()) {
				if (predicate(entity))
					return entity;
			}

			return Option<GameEntity>.None;
		}

		public static void forEach(this IGroup<GameEntity> self, Action<GameEntity> action) {
			foreach (var entity in self.GetEntities()) {
				action.Invoke(entity);
			}
		}
		
		public static void forEach(this IEnumerable<GameEntity> self, Action<GameEntity> action) {
			foreach (var entity in self) {
				action.Invoke(entity);
			}
		}
		
		public static IEnumerable<GameEntity> where(
			this IGroup<GameEntity> self, Func<GameEntity, bool> predicate
		) => self.GetEntities().Where(predicate);

		public static bool any(
			this IGroup<GameEntity> self, Func<GameEntity, bool> predicate
		) {
			foreach (var entity in self.GetEntities()) {
				if (predicate(entity))
					return true;
			}

			return false;
		}

		public static bool isSamePoint(this GameEntity @this, GameEntity with) =>
			@this.pathIndex.value == with.pathIndex.value &&
			@this.pointIndex.value == with.pointIndex.value;

		public static bool onPoint(this GameEntity @this, GameEntity point) =>
			@this.pathIndex.value == point.pathIndex.value &&
			@this.pointIndex.value == point.pointIndex.value &&
			@this.pointIndex.value == point.previousPointIndex.value;

		public static bool isSamePoint(this GameEntity @this, int pathIndex, int pointIndex) =>
			@this.pathIndex.value == pathIndex && @this.pointIndex.value == pointIndex;
	}
}