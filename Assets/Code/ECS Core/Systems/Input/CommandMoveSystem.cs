using Entitas;
using LanguageExt;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;
using Rewind.Extensions;
using Rewind.Services;
using static LanguageExt.Prelude;

public class CommandMoveSystem : IExecuteSystem {
	readonly InputContext input;
	readonly GameContext game;
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> points;

	public CommandMoveSystem(Contexts contexts) {
		input = contexts.input;
		game = contexts.game;
		players = game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CurrentPoint));
		points = game.GetGroup(GameMatcher.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.PointOpenStatus));
	}

	public void Execute() {
		var clockState = game.clockEntity.clockState.value;
		if (clockState.isRewind()) return;

		var maybeDirection = input.input.getMoveDirection().flatMap(direction => direction.asHorizontal());
		foreach (var player in players.GetEntities()) {
			var currentPoint = player.currentPoint.value;
			var maybePreviousPoint = player.maybeValue(p => p.hasPreviousPoint, p => p.previousPoint.value);

			maybeDirection
				.flatMap(direction => maybeNextPoint(currentPoint, maybePreviousPoint, direction))
				.Match(
					Some: nextPoint => {
						if (clockState.isRecord()) {
							game.createMoveTimePoint(
								currentPoint: nextPoint, previousPoint: currentPoint,
								rewindPoint: maybePreviousPoint.IfNone(currentPoint)
							);
						}

						if (maybePreviousPoint.Map(previousPoint => previousPoint == nextPoint).IfNone(false)) {
							player.ReplaceTraveledValue(1 - player.traveledValue.clampedValue());
						}

						player
							.ReplaceCurrentPoint(nextPoint)
							.ReplacePreviousPoint(currentPoint);
						// .ReplaceMoveComplete(false);

						if (player.isMoveComplete()) {
							player.ReplaceMoveComplete(false);
						}
					},
					None: () => {
						if (player.isTargetReached != player.isMoveComplete()) {
							player.ReplaceMoveComplete(player.isTargetReached);
						}
					}
				);
		}
	}

	Option<PathPoint> maybeNextPoint(
		PathPoint currentPoint, Option<PathPoint> maybePreviousPoint, HorizontalMoveDirection direction
	) {
		var nextPoint = currentPoint.pathWithIndex(currentPoint.index + direction.intValue());
		
		return canReach() && isSamePath() && canMoveFromThisPoint() && canMoveToNextPoint()
			? Some(nextPoint)
			: None;
		
		bool canReach() => maybePreviousPoint.Map(pp => (nextPoint.index - pp.index).abs() < 2).IfNone(true);

		bool isSamePath() => maybePreviousPoint.Map(pp => currentPoint.pathId == pp.pathId).IfNone(true);

		bool canMoveFromThisPoint() => points
			.first(p => p.isSamePoint(currentPoint))
			.Map(p => p.pointOpenStatus.value.openToMoveFromWithDirection(direction))
			.IfNone(false);

		bool canMoveToNextPoint() => points
			.first(p => p.isSamePoint(nextPoint))
			.Map(p => p.pointOpenStatus.value.openToMoveToWithDirection(direction))
			.IfNone(false);
	}
}
