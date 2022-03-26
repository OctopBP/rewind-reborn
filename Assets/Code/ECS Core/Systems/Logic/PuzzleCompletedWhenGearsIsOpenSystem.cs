using System.Linq;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class PuzzleCompletedWhenGearsIsOpenSystem : IExecuteSystem {
	readonly IGroup<GameEntity> puzzleGroups;
	readonly IGroup<GameEntity> gears;

	public PuzzleCompletedWhenGearsIsOpenSystem(Contexts contexts) {
		gears = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.GearTypeA, GameMatcher.GearTypeAState));
		puzzleGroups = contexts.game.GetGroup(GameMatcher.AllOf(
				GameMatcher.PuzzleGroup, GameMatcher.PuzzleOutputs
			).NoneOf(
				GameMatcher.PuzzleComplete
			)
		);
	}

	public void Execute() {
		foreach (var puzzleGroup in puzzleGroups.GetEntities()) {
			if (gears.count > 0 && gears.where(g => puzzleGroup.puzzleInputs.value.Contains(g.id.value))
			    .All(g => g.gearTypeAState.value.isOpened())) {
				puzzleGroup.isPuzzleComplete = true;
			}
		}
	}
}