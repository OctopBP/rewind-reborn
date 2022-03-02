using Entitas;

public class MoveSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> pathFollowers;

	public MoveSystem(Contexts contexts) {
		game = contexts.game;

		pathFollowers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.MoveTarget, GameMatcher.PathFollower,
			GameMatcher.PathFollowerSpeed, GameMatcher.Position
		));
	}

	public void Execute() {
		foreach (var pathFollower in pathFollowers.GetEntities()) {
			var direction = pathFollower.moveTarget.value - pathFollower.position.value;
			var deltaMove = direction.normalized * pathFollower.pathFollowerSpeed.value *
			                game.worldTime.value.deltaTime;

			if (deltaMove.magnitude >= direction.magnitude) {
				pathFollower.ReplacePosition(pathFollower.moveTarget.value);
				pathFollower.isMoveComplete = true;
			} else {
				var newPosition = pathFollower.position.value + deltaMove;
				pathFollower.ReplacePosition(newPosition);
				pathFollower.isMoveComplete = false;
			}
		}
	}
}