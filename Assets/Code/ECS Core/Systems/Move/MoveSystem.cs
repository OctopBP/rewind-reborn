using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

public class MoveSystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly IGroup<GameEntity> pathFollowers;

	public MoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		pathFollowers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.MoveTarget, GameMatcher.PathFollower, GameMatcher.PathFollowerSpeed, GameMatcher.Position,
			GameMatcher.MoveState
		));
	}

	public void Execute() {
		foreach (var pathFollower in pathFollowers.GetEntities()) {
			var direction = pathFollower.moveTarget.value - pathFollower.position.value;
			var deltaMove = direction.normalized * pathFollower.pathFollowerSpeed.value * clock.deltaTime.value;
			var moveState = pathFollower.moveState.value;
			
			var targetReached = deltaMove.magnitude >= direction.magnitude;
			var newPosition = targetReached || moveState.isStanding()
				? pathFollower.moveTarget.value
				: pathFollower.position.value + deltaMove;

			pathFollower
				.with(pf => pf.ReplacePosition(newPosition))
				.with(pf => pf.isMoveComplete = targetReached);
		}
	}
}