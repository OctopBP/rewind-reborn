using System;
using Entitas;
using Rewind.ECSCore.Enums;
using GM = GameMatcher;

public class CheckPuzzleElementsIsDoneSystem : IExecuteSystem {
	readonly (IGroup<GameEntity> group, Func<GameEntity, bool> func)[] groups;

	public CheckPuzzleElementsIsDoneSystem(Contexts contexts) {
		groups = new (IGroup<GameEntity> group, Func<GameEntity, bool> func)[] {
			(puzzleElementWithTypes(GM.GearTypeA, GM.GearTypeAState), gear => gear.gearTypeAState.value.isOpened()),
			(puzzleElementWithTypes(GM.LeverA, GM.LeverAState), lever => lever.leverAState.value.isOpened()),
			(puzzleElementWithTypes(GM.ButtonA, GM.ButtonAState), button => button.buttonAState.value.isOpened())
		};

		IGroup<GameEntity> puzzleElementWithTypes(params IMatcher<GameEntity>[] matchers) =>
			contexts.game.GetGroup(GM.AllOf(GM.AllOf(GM.Id, GM.PuzzleElement), GM.AllOf(matchers)));
	}

	public void Execute() {
		foreach (var (group, func) in groups) {
			foreach (var element in group.GetEntities()) {
				element.isPuzzleElementDone = func(element);
			}
		}
	}
}