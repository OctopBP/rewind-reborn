using Entitas;
using Rewind.ECSCore.Enums;

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
			var deltaMove = direction.normalized * pathFollower.pathFollowerSpeed.value *
			                clock.deltaTime.value;

			var lastMove = deltaMove.magnitude >= direction.magnitude;
			pathFollower.ReplacePosition(lastMove || pathFollower.moveState.value.isStanding()
				? pathFollower.moveTarget.value
				: pathFollower.position.value + deltaMove
			);
			pathFollower.isMoveComplete = lastMove;
		}
	}
}