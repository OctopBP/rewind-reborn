using System;
using Entitas;
using LanguageExt;

namespace Rewind.Services {
	public static partial class EntitasExtensions {
		public static Option<GameEntity> first(
			this IGroup<GameEntity> self, Func<GameEntity, bool> predicate
		) {
			foreach (var entity in self.GetEntities()) {
				if (predicate(entity))
					return entity;
			}

			return Option<GameEntity>.None;
		}
		
		public static bool isSamePoint(this GameEntity @this, GameEntity with) =>
			@this.pathIndex.value == with.pathIndex.value &&
			@this.pointIndex.value == with.pointIndex.value;

		public static bool isSamePoint(this GameEntity @this, int pathIndex, int pointIndex) =>
			@this.pathIndex.value == pathIndex && @this.pointIndex.value == pointIndex;
	}
}