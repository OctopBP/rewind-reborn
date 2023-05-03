using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class ActivatePlatformAWhenPuzzleCompletedSystem : IExecuteSystem {
	readonly IGroup<GameEntity> puzzleGroups;
	readonly IGroup<GameEntity> platforms;

	public ActivatePlatformAWhenPuzzleCompletedSystem(Contexts contexts) {
		platforms = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PlatformA, GameMatcher.PlatformAState, GameMatcher.Id)
		);
		puzzleGroups = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PuzzleGroup, GameMatcher.PuzzleOutputs
		));
	}

	public void Execute() {
		foreach (var puzzleGroup in puzzleGroups.GetEntities()) {
			foreach (var platform in platforms.where(p => puzzleGroup.puzzleOutputs.value.Contains(p.id.value))) {
				platform.ReplacePlatformAState(puzzleGroup.isPuzzleComplete
					? PlatformAState.Active
					: PlatformAState.NotActive
				);
			}
		}
	}
}