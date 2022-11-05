using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.ECSCore.Helpers;
using Rewind.Extensions;
using Rewind.Services;
using static LanguageExt.Prelude;

public class CommandMoveSystem : IExecuteSystem {
	readonly InputContext input;
	readonly GameContext game;
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> points;
	readonly GameEntity clock;

	public CommandMoveSystem(Contexts contexts) {
		input = contexts.input;
		game = contexts.game;
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CurrentPoint));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.PointOpenStatus
		));
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		var maybeDirection = getMoveDirection();

		maybeDirection.IfSome(direction => {
			foreach (var player in players.GetEntities()) {
				var currentPoint = player.currentPoint.value;

				var nextPointIndex = currentPoint.index + direction.intValue();
				var maybePreviousPoint = player.maybeValue(p => p.hasPreviousPoint, p => p.previousPoint.value);

				if (maybePreviousPoint.Match(
				    pp => (nextPointIndex - pp.index).abs() >= 2 || currentPoint.pathId != pp.pathId, 
				    () => false
				)) continue;

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

					replacePoints(player, point: newPoint, previousPoint: currentPoint);

					void replacePoints(GameEntity entity, PathPoint point, PathPoint previousPoint) => entity
						.with(e => e.ReplaceCurrentPoint(point))
						.with(e => e.ReplacePreviousPoint(previousPoint));
				}
			}
		});
	}

	Option<MoveDirection> getMoveDirection() {
		var inputService = input.input.value;

		if (inputService.getMoveRightButton()) return MoveDirection.Right;
		if (inputService.getMoveLeftButton()) return MoveDirection.Left;

		return None;
	}
}
