using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class ActivatePendulumWhenPuzzleCompletedSystem : IExecuteSystem {
	readonly IGroup<GameEntity> puzzleGroups;
	readonly IGroup<GameEntity> pendulums;

	public ActivatePendulumWhenPuzzleCompletedSystem(Contexts contexts) {
		pendulums = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Pendulum, GameMatcher.PendulumState, GameMatcher.Id)
		);
		puzzleGroups = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PuzzleGroup, GameMatcher.PuzzleComplete, GameMatcher.PuzzleOutputs
		));
	}

	public void Execute() {
		foreach (var puzzleGroup in puzzleGroups.GetEntities()) {
			foreach (var pendulum in pendulums.where(p => puzzleGroup.puzzleOutputs.value.Contains(p.id.value))) {
				pendulum.ReplacePendulumState(PendulumState.Active);
			}
		}
	}
}