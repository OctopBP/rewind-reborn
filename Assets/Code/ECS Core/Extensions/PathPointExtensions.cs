using System;
using Entitas;
using LanguageExt;

namespace Rewind.Services
{
	public static class PathPointExtensions
	{
		public static Option<GameEntity> MaybeFind(this IGroup<GameEntity> @this, PathPoint pathPoint) =>
			@this.First(p => p.IsSamePoint(pathPoint));

		public static Option<GameEntity> FindPointOf(this IGroup<GameEntity> group, GameEntity with) =>
			group.First(p => p.currentPoint.value == with.currentPoint.value);
		
		public static Option<GameEntity> FindPointOf(this IGroup<GameEntity> group, PathPoint pathPoint) =>
			group.First(p => p.currentPoint.value == pathPoint);

		public static bool IsSamePoint(this GameEntity @this, GameEntity with) =>
			@this.currentPoint.value == with.currentPoint.value;
		
		public static bool IsSamePoint(this GameEntity @this, PathPoint pathPoint) =>
			@this.currentPoint.value == pathPoint;

		public static bool IsSamePoint(this PathPoint @this, GameEntity entity) =>
			entity.currentPoint.value == @this;

		public static bool IsSamePoint(this GameEntity @this, Guid pathId, int pointIndex) =>
			@this.currentPoint.value.pathId == pathId && @this.currentPoint.value.index == pointIndex;
	}
}