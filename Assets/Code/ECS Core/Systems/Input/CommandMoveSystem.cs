using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.ECSCore.Helpers;
using Rewind.Extensions;
using Rewind.Services;
using UnityEngine;

public class CommandMoveSystem : IExecuteSystem {
	readonly InputContext input;
	readonly GameContext game;
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> points;
	readonly GameEntity clock;

	public CommandMoveSystem(Contexts contexts) {
		input = contexts.input;
		game = contexts.game;
		players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CurrentPoint));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.PointOpenStatus
		));
		clock = contexts.game.clockEntity;
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		var maybeDirection = input.input.getMoveDirection().Filter(direction => direction.isHorizontal());
		foreach (var player in players.GetEntities()) {
			maybeDirection.Match(
				Some: direction => {
					var currentPoint = player.currentPoint.value;

					var nextPointIndex = currentPoint.index + direction.intValue();
					var maybePreviousPoint = player.maybeValue(p => p.hasPreviousPoint, p => p.previousPoint.value);

					if (maybePreviousPoint.Match(
					    pp => (nextPointIndex - pp.index).abs() >= 2 || currentPoint.pathId != pp.pathId, 
					    () => false
					)) return;

					var canMoveFromThisPoint = points
						.first(player.isSamePoint)
						.Map(p => direction.ableToGoFromPoint(p.pointOpenStatus.value))
						.IfNone(false);

					var canMoveToNextPoint = points
						.first(p => p.isSamePoint(currentPoint.pathId, nextPointIndex))
						.Map(p => direction.ableToGoToPoint(p.pointOpenStatus.value))
						.IfNone(false);

					if (canMoveFromThisPoint && canMoveToNextPoint) {
						var newPoint = currentPoint.pathWithIndex(nextPointIndex);

						if (game.clockEntity.clockState.value.isRecord()) {
							game.createMoveTimePoint(
								currentPoint: newPoint, previousPoint: currentPoint,
								rewindPoint: maybePreviousPoint.IfNone(currentPoint)
							);
						}

						replacePoints(player, currentPoint: newPoint, previousPoint: currentPoint);
						
						player
							.with(e => e.isMoveComplete = false)
							.with(e => e.ReplaceMoveState(direction.asMoveState()));
						
						void replacePoints(GameEntity entity, PathPoint currentPoint, PathPoint previousPoint) => entity
							.with(e => e.ReplaceCurrentPoint(currentPoint))
							.with(e => e.ReplacePreviousPoint(previousPoint));
					} else {
						player
							.with(e => e.isMoveComplete = player.isTargetReached)
							.with(e => e.ReplaceMoveState(MoveState.None));
					}
				},
				None: () => {
					player
						.with(e => e.isMoveComplete = player.isTargetReached)
						.with(e => e.ReplaceMoveState(MoveState.None));
				}
			);
		}
	}
}
