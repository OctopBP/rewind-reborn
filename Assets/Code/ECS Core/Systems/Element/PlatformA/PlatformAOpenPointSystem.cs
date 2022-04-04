using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;
using UnityEngine;

public class PlatformAOpenPointSystem : IExecuteSystem {
	readonly IGroup<GameEntity> platforms;
	readonly IGroup<GameEntity> points;

	public PlatformAOpenPointSystem(Contexts contexts) {
		platforms = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PlatformA, GameMatcher.PlatformAData, GameMatcher.PlatformAMoveTime, GameMatcher.PointIndex
		));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.PointOpenStatus
		));
	}

	public void Execute() {
		foreach (var platform in platforms.GetEntities()) {
			points.first(platform.isSamePoint).IfSome(point => {
				var newOpenStatus = PointOpenStatus.Opened;

				var canMoveRight = canMoveToPoint(point, platform, indexOffset: 1);
				var canMoveLeft = canMoveToPoint(point, platform, indexOffset: -1);

				if (!canMoveRight) newOpenStatus |= PointOpenStatus.ClosedRight;
				if (!canMoveLeft) newOpenStatus |= PointOpenStatus.ClosedLeft;

				point.ReplacePointOpenStatus(newOpenStatus);
			});
		}

		bool canMoveToPoint(GameEntity point, GameEntity platform, int indexOffset) =>
			checkOpenLimit(
				point.pointIndex.value, point.position.value, platform.platformAData.value.openLimit, indexOffset
			);

		bool checkOpenLimit(PathPointType pathPoint, Vector2 position, float openLimit, int indexOffset) =>
			points.first(p => p.isSamePoint(pathPoint.pathId, pathPoint.index + indexOffset)).Match(
				p => Vector2.Distance(position, p.position.value) > openLimit,
				() => false
			);
	}
}