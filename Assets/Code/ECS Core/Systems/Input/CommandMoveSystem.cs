using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

public class CommandMoveSystem : IExecuteSystem {
	readonly GameContext game;
	readonly InputContext input;
	readonly IGroup<GameEntity> pathFollowers;
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> connectors;

	public CommandMoveSystem(Contexts contexts) {
		game = contexts.game;
		input = contexts.input;

		pathFollowers = game.GetGroup(
			GameMatcher.AllOf(GameMatcher.PathFollower, GameMatcher.PathIndex, GameMatcher.PointIndex)
		);

		points = game.GetGroup(
			GameMatcher.AllOf(
				GameMatcher.Point, GameMatcher.PathIndex, GameMatcher.PointIndex, GameMatcher.Position
			)
		);
	}

	public void Execute() {
		var moveDirection = getMoveDirection();
		if (!moveDirection.valueOut(out var direction)) return;

		foreach (var pathFollower in pathFollowers.GetEntities()) {
			var nextPointIndex = pathFollower.pointIndex.value + direction.intValue();

			if ((nextPointIndex - pathFollower.previousPointIndex.value).abs() >= 2) continue;

			var currentPoint = points.first(pathFollower.isSamePoint);
			var canMoveFromThisPoint = currentPoint.Match(
				p => direction.map(onLeft: !p.isBlockPrevious, onRight: !p.isBlockNext), () => false
			);

			var targetPoint = points.first(p => p.isSamePoint(pathFollower.pathIndex.value, nextPointIndex));
			var canMoveToNextPoint = targetPoint.Match(
				p => direction.map(onLeft: !p.isBlockNext, onRight: !p.isBlockPrevious), () => false
			);

			if (canMoveFromThisPoint && canMoveToNextPoint) {
				// CreateTimePoint(player.pointIndex.Value, nextPointIndex, player.pathIndex.Value, player.pathIndex.Value);

				pathFollower.ReplacePreviousPointIndex(pathFollower.pointIndex.value);
				pathFollower.ReplacePointIndex(nextPointIndex);

				continue;
			}
		}
	}

	Option<MoveDirection> getMoveDirection() {
		if (input.input.value.getMoveRightButton())
			return MoveDirection.Right;

		if (input.input.value.getMoveLeftButton())
			return MoveDirection.Left;

		return Option<MoveDirection>.None;
	}
}