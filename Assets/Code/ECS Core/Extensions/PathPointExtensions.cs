using System;
using Entitas;
using LanguageExt;

namespace Rewind.Services {
	public static class PathPointExtensions {
		public static Option<GameEntity> maybeFind(this IGroup<GameEntity> @this, PathPointType pathPoint) =>
			@this.first(p => p.isSamePoint(pathPoint));

		public static bool isSamePoint(this GameEntity @this, GameEntity with) =>
			@this.pointIndex.value == with.pointIndex.value;
		
		public static bool isSamePoint(this GameEntity @this, PathPointType pathPoint) =>
			@this.pointIndex.value == pathPoint;

		public static bool isSamePoint(this PathPointType @this, GameEntity entity) =>
			entity.pointIndex.value == 	@this;

		public static bool onPoint(this GameEntity @this, GameEntity point) =>
			@this.pointIndex.value == point.pointIndex.value &&
			@this.pointIndex.value == point.previousPointIndex.value;

		public static bool isSamePoint(this GameEntity @this, Guid pathId, int pointIndex) =>
			@this.pointIndex.value.pathId == pathId && @this.pointIndex.value.index == pointIndex;
	}
}