using Entitas;
using Rewind.ECSCore.Enums;

public class CheckPuzzleElementsIsDoneSystem : IExecuteSystem {
	readonly IGroup<GameEntity> gears;
	readonly IGroup<GameEntity> levers;
	readonly IGroup<GameEntity> buttons;

	public CheckPuzzleElementsIsDoneSystem(Contexts contexts) {
		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Id, GameMatcher.PuzzleElement, GameMatcher.GearTypeA, GameMatcher.GearTypeAState
		));
		levers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Id, GameMatcher.PuzzleElement, GameMatcher.LeverA, GameMatcher.LeverAState
		));
		buttons = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Id, GameMatcher.PuzzleElement, GameMatcher.ButtonA, GameMatcher.ButtonAState
		));
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) gear.isPuzzleElementDone = gear.gearTypeAState.value.isOpened();
		foreach (var lever in levers.GetEntities()) lever.isPuzzleElementDone = lever.leverAState.value.isOpened();
		foreach (var button in buttons.GetEntities()) button.isPuzzleElementDone = button.buttonAState.value.isOpened();
	}
}