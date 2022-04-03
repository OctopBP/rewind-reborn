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
			GameMatcher.PuzzleGroup, GameMatcher.PuzzleOutputs)
		);
	}

	public void Execute() {
		foreach (var puzzleGroup in puzzleGroups.GetEntities()) {
			var done = levers.count > 0 && levers
				.where(g => puzzleGroup.puzzleInputs.value.Contains(g.id.value))
				.All(g => g.leverAState.value.isOpened());

			puzzleGroup.isPuzzleComplete = puzzleGroup.isPuzzleGroupRepeatable
				? done
				: done || puzzleGroup.isPuzzleComplete;
		}
	}
}