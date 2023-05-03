using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class ActivateDoorAWhenPuzzleCompletedSystem : IExecuteSystem {
	readonly IGroup<GameEntity> puzzleGroups;
	readonly IGroup<GameEntity> doors;

	public ActivateDoorAWhenPuzzleCompletedSystem(Contexts contexts) {
		doors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.DoorA, GameMatcher.DoorAState, GameMatcher.Id)
		);
		puzzleGroups = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PuzzleGroup, GameMatcher.PuzzleOutputs
		));
	}

	public void Execute() {
		foreach (var puzzleGroup in puzzleGroups.GetEntities()) {
			foreach (var door in doors.where(p => puzzleGroup.puzzleOutputs.value.Contains(p.id.value))) {
				door.ReplaceDoorAState(puzzleGroup.isPuzzleComplete
					? DoorAState.Opened
					: DoorAState.Closed
				);
			}
		}
	}
}