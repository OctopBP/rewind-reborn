using Entitas;
using Rewind.ECSCore.Enums;

public class MoveStateSystem : IExecuteSystem {
	readonly IGroup<GameEntity> pathFollowers;

	public MoveStateSystem(Contexts contexts) {
		pathFollowers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PathFollower, GameMatcher.CurrentPoint, GameMatcher.PreviousPoint,
			GameMatcher.MoveState
		));
	}

	public void Execute() {
		foreach (var pf in pathFollowers.GetEntities()) {
			var isStaying = pf.currentPoint.value == pf.previousPoint.value;
			pf.ReplaceMoveState(isStaying ? MoveState.Standing : MoveState.Moving);
		}
	}
}