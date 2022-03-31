using Entitas;
using Rewind.ECSCore.Enums;

public class MoveStateSystem : IExecuteSystem {
	readonly IGroup<GameEntity> pathFollowers;

	public MoveStateSystem(Contexts contexts) {
		pathFollowers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PathFollower, GameMatcher.PointIndex, GameMatcher.PreviousPointIndex,
			GameMatcher.MoveState
		));
	}

	public void Execute() {
		foreach (var pathFollower in pathFollowers.GetEntities()) {
			var isStaying = pathFollower.pointIndex.value == pathFollower.previousPointIndex.value;
			pathFollower.ReplaceMoveState(isStaying ? MoveState.Standing : MoveState.Moving);
		}
	}
}