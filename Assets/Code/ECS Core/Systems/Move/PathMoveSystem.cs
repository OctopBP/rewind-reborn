using Entitas;
using Rewind.Services;

public class PathMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> moveTargets;

	public PathMoveSystem(Contexts contexts) {
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.Position
		));

		moveTargets = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Movable, GameMatcher.CurrentPoint
		));
	}

	public void Execute() {
		moveTargets.GetEntities().forEach(moveTarget => points
			.first(moveTarget.isSamePoint)
			.IfSome(point => moveTarget.ReplaceMoveTarget(point.position.value)
		));
	}
}