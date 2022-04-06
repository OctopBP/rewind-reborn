using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

public class CommandMoveSystem : IExecuteSystem {
	readonly InputContext input;
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> points;
	readonly GameEntity clock;

	public CommandMoveSystem(Contexts contexts) {
		input = contexts.input;
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.PointIndex));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.PointOpenStatus
		));
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		getMoveDirection().IfSome(direction => {
			foreach (var player in players.GetEntities()) {
				var nextPointIndex = player.pointIndex.value.index + direction.intValue();

				if ((nextPointIndex - player.previousPointIndex.value.index).abs() < 2 &&
				    player.pointIndex.value.pathId == player.previousPointIndex.value.pathId
				) {
					var currentPoint = points.first(player.isSamePoint);
					var canMoveFromThisPoint = currentPoint
						.Some(p => direction.map(
							onLeft: p.pointOpenStatus.value.isOpenLeft(),
							onRight: p.pointOpenStatus.value.isOpenRight()
						))
						.None(() => false);

					var canMoveToNextPoint = points
						.first(p => p.isSamePoint(player.pointIndex.value.pathId, nextPointIndex))
						.Some(p => direction.map(
							onLeft: p.pointOpenStatus.value.isOpenRight(),
							onRight: p.pointOpenStatus.value.isOpenLeft()
						))
						.None(() => false);

					if (canMoveFromThisPoint && canMoveToNextPoint) {
						player.ReplaceRewindPointIndex(player.previousPointIndex.value);
						player.ReplacePreviousPointIndex(player.pointIndex.value);
						player.ReplacePointIndex(new(player.pointIndex.value.pathId, nextPointIndex));
					}
				}
			}
		});
	}

	Option<MoveDirection> getMoveDirection() {
		var inputService = input.input.value;

		if (inputService.getMoveRightButton()) return MoveDirection.Right;
		if (inputService.getMoveLeftButton()) return MoveDirection.Left;

		return Option<MoveDirection>.None;
	}
}
