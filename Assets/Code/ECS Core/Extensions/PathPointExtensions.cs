using System;
using Entitas;
using LanguageExt;

namespace Rewind.Services {
	public static class PathPointExtensions {
		public static Option<GameEntity> maybeFind(this IGroup<GameEntity> @this, PathPoint pathPoint) =>
			@this.first(p => p.isSamePoint(pathPoint));

		public static bool isSamePoint(this GameEntity @this, GameEntity with) =>
			@this.currentPoint.value == with.currentPoint.value;
		
		public static bool isSamePoint(this GameEntity @this, PathPoint pathPoint) =>
			@this.currentPoint.value == pathPoint;

		public static bool isSamePoint(this PathPoint @this, GameEntity entity) =>
			entity.currentPoint.value == @this;

		public static bool isSamePoint(this GameEntity @this, Guid pathId, int pointIndex) =>
			@this.currentPoint.value.pathId == pathId && @this.currentPoint.value.index == pointIndex;
	}
}