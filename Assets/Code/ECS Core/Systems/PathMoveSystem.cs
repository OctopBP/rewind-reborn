using Entitas;
using Rewind.Extensions;
using Rewind.Services;

public class PathMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> moveTargets;

	public PathMoveSystem(Contexts contexts) {
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PathIndex, GameMatcher.PointIndex, GameMatcher.Position
		));

		moveTargets = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Move, GameMatcher.PathIndex, GameMatcher.PointIndex
		));
	}

	public void Execute() {
		foreach (var moveTarget in moveTargets.GetEntities()) {
			var maybePoint = points.first(moveTarget.isSamePoint);

			if (maybePoint.valueOut(out var point)) {
				moveTarget.ReplaceMoveTarget(point.position.value);
			}
		}
	}
}