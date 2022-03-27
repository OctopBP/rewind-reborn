using Entitas;
using Rewind.ECSCore.Enums;

public class MoveSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> pathFollowers;

	public MoveSystem(Contexts contexts) {
		game = contexts.game;
		pathFollowers = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.MoveTarget, GameMatcher.PathFollower, GameMatcher.PathFollowerSpeed, GameMatcher.Position,
			GameMatcher.MoveState
		));
	}

	public void Execute() {
		foreach (var pathFollower in pathFollowers.GetEntities()) {
			var direction = pathFollower.moveTarget.value - pathFollower.position.value;
			var deltaMove = direction.normalized * pathFollower.pathFollowerSpeed.value *
			                game.worldTime.value.deltaTime;

			var lastMove = deltaMove.magnitude >= direction.magnitude;
			pathFollower.ReplacePosition(lastMove || pathFollower.moveState.value.isStanding()
				? pathFollower.moveTarget.value
				: pathFollower.position.value + deltaMove
			);
			pathFollower.isMoveComplete = lastMove;
		}
	}
}