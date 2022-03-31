using Entitas;
using Rewind.Services;

public class PathMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> moveTargets;

	public PathMoveSystem(Contexts contexts) {
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.Position
		));

		moveTargets = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Movable, GameMatcher.PointIndex
		));
	}

	public void Execute() {
		foreach (var moveTarget in moveTargets.GetEntities()) {
			points.first(moveTarget.isSamePoint)
				.IfSome(point => moveTarget.ReplaceMoveTarget(point.position.value));
		}
	}
}