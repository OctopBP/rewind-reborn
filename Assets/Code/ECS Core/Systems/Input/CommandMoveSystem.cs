using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

public class CommandMoveSystem : IExecuteSystem {
	readonly InputContext input;
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> connectors;
	readonly GameEntity clock;

	public CommandMoveSystem(Contexts contexts) {
		input = contexts.input;
		clock = contexts.game.clockEntity;

		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.PathIndex, GameMatcher.PointIndex
		));

		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PathIndex, GameMatcher.PointIndex, GameMatcher.PointOpenStatus
		));
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		getMoveDirection().IfSome(direction => {
			foreach (var player in players.GetEntities()) {
				var nextPointIndex = player.pointIndex.value + direction.intValue();

				if ((nextPointIndex - player.previousPointIndex.value).abs() < 2) {
					var currentPoint = points.first(player.isSamePoint);
					var canMoveFromThisPoint = currentPoint.Match(
						p => direction.map(
							onLeft: p.pointOpenStatus.value.isOpenLeft(),
							onRight: p.pointOpenStatus.value.isOpenRight()
						),
						() => false
					);

					var targetPoint = points.first(
						p => p.isSamePoint(player.pathIndex.value, nextPointIndex)
					);
					var canMoveToNextPoint = targetPoint.Match(
						p => direction.map(
							onLeft: p.pointOpenStatus.value.isOpenRight(),
							onRight: p.pointOpenStatus.value.isOpenLeft()
						),
						() => false
					);

					if (canMoveFromThisPoint && canMoveToNextPoint) {
						player.ReplaceRewindPointIndex(player.previousPointIndex.value);
						player.ReplacePreviousPointIndex(player.pointIndex.value);
						player.ReplacePointIndex(nextPointIndex);
					}
				}
			}
		});
	}

	Option<MoveDirection> getMoveDirection() {
		if (input.input.value.getMoveRightButton())
			return MoveDirection.Right;

		if (input.input.value.getMoveLeftButton())
			return MoveDirection.Left;

		return Option<MoveDirection>.None;
	}
}
