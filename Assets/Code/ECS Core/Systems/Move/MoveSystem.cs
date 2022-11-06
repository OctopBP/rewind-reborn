using Entitas;
using Rewind.Extensions;

public class MoveSystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly IGroup<GameEntity> pathFollowers;

	public MoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		pathFollowers = contexts.game.GetGroup(GameMatcher
			.AllOf(
				GameMatcher.MoveTarget, GameMatcher.PathFollower, GameMatcher.PathFollowerSpeed, GameMatcher.Position
			)
		);
	}

	public void Execute() {
		foreach (var pathFollower in pathFollowers.GetEntities()) {
			var direction = pathFollower.moveTarget.value - pathFollower.position.value;
			var deltaMove = direction.normalized * pathFollower.pathFollowerSpeed.value * clock.deltaTime.value;

			var targetReached = deltaMove.magnitude >= direction.magnitude;
			var newPosition = targetReached
				? pathFollower.moveTarget.value
				: pathFollower.position.value + deltaMove;

			pathFollower
				.with(pf => pf.ReplacePosition(newPosition))
				.with(pf => pf.isTargetReached = targetReached);
		}
	}
}