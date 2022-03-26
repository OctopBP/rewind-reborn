using System.Linq;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class PuzzleCompletedWhenButtonsAIsOpenSystem : IExecuteSystem {
	readonly IGroup<GameEntity> puzzleGroups;
	readonly IGroup<GameEntity> buttons;

	public PuzzleCompletedWhenButtonsAIsOpenSystem(Contexts contexts) {
		buttons = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.ButtonA, GameMatcher.ButtonAState));
		puzzleGroups = contexts.game.GetGroup(GameMatcher.AllOf(
				GameMatcher.PuzzleGroup, GameMatcher.PuzzleOutputs
			).NoneOf(
				GameMatcher.PuzzleComplete
			)
		);
	}

	public void Execute() {
		foreach (var puzzleGroup in puzzleGroups.GetEntities()) {
			if (buttons.count > 0 && buttons.where(g => puzzleGroup.puzzleInputs.value.Contains(g.id.value))
			    .All(g => g.buttonAState.value.isOpened())) {
				puzzleGroup.isPuzzleComplete = true;
			}
		}
	}
}