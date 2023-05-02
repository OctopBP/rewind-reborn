using System.Linq;
using Entitas;
using Rewind.Services;

public class PuzzleCompletedWhenElementsAIsDoneSystem : IExecuteSystem {
	readonly IGroup<GameEntity> groups;
	readonly IGroup<GameEntity> elements;

	public PuzzleCompletedWhenElementsAIsDoneSystem(Contexts contexts) {
		elements = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Id, GameMatcher.PuzzleElement));
		groups = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.PuzzleGroup, GameMatcher.PuzzleInputs));
	}

	public void Execute() {
		foreach (var puzzleGroup in groups.GetEntities()) {
			var groupElements = elements.where(e => puzzleGroup.puzzleInputs.value.Contains(e.id.value)).ToList();
			var puzzleComplete = groupElements.Any(e => e.isPuzzleElementDone) &&
			                     (groupElements.All(e => e.isPuzzleElementDone) || puzzleGroup.isPuzzleGroupAnyInput);

			puzzleGroup.isPuzzleComplete = puzzleGroup.isPuzzleGroupRepeatable
				? puzzleComplete
				: puzzleComplete || puzzleGroup.isPuzzleComplete;
		}
	}
}