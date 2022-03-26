using System.Linq;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class PuzzleCompletedWhenLeverAIsOpenSystem : IExecuteSystem {
	readonly IGroup<GameEntity> puzzleGroups;
	readonly IGroup<GameEntity> levers;

	public PuzzleCompletedWhenLeverAIsOpenSystem(Contexts contexts) {
		levers = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.LeverA, GameMatcher.LeverAState));
		puzzleGroups = contexts.game.GetGroup(GameMatcher.AllOf(
				GameMatcher.PuzzleGroup, GameMatcher.PuzzleOutputs
			).NoneOf(
				GameMatcher.PuzzleComplete
			)
		);
	}

	public void Execute() {
		foreach (var puzzleGroup in puzzleGroups.GetEntities()) {
			if (levers.count > 0 && levers.where(g => puzzleGroup.puzzleInputs.value.Contains(g.id.value))
			    .All(g => g.leverAState.value.isOpened())) {
				puzzleGroup.isPuzzleComplete = true;
			}
		}
	}
}