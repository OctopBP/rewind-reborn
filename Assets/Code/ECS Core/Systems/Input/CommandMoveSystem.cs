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
		players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CurrentPoint));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.PointOpenStatus
		));
		clock = contexts.game.clockEntity;
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		var maybeDirection = input.input.getMoveDirection().Map(direction => direction.asHorizontal()).Flatten();
		foreach (var player in players.GetEntities()) {
			var currentPoint = player.currentPoint.value;
			var maybePreviousPoint = player.maybeValue(p => p.hasPreviousPoint, p => p.previousPoint.value);

			maybeDirection
				.Map(direction => maybeNextPoint(currentPoint, maybePreviousPoint, direction))
				.Flatten()
				.Match(
					Some: nextPoint => {
						if (game.clockEntity.clockState.value.isRecord()) {
							game.createMoveTimePoint(
								currentPoint: nextPoint, previousPoint: currentPoint,
								rewindPoint: maybePreviousPoint.IfNone(currentPoint)
							);
						}

						replacePoints(player, currentPoint: nextPoint, previousPoint: currentPoint);
						
						player.with(apply: e => e.ReplaceMoveComplete(false), when: player.isMoveComplete());
					},
					None: () => player.with(
						apply: e => e.ReplaceMoveComplete(player.isTargetReached),
						when: player.isTargetReached != player.isMoveComplete())
					);
		}
		
		void replacePoints(GameEntity entity, PathPoint currentPoint, PathPoint previousPoint) => entity
			.with(e => e.ReplaceCurrentPoint(currentPoint))
			.with(e => e.ReplacePreviousPoint(previousPoint));
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
