using System.Linq;
using Entitas;
using Rewind.Extensions;
using Rewind.Services;
using UnityEngine;

public class CheckPuzzleGearTypeCIsDoneSystem : IExecuteSystem {
	readonly IGroup<GameEntity> gears;
	readonly IGroup<GameEntity> groups;
	bool temp;

	public CheckPuzzleGearTypeCIsDoneSystem(Contexts contexts) {
		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Id, GameMatcher.PuzzleElement, GameMatcher.GearTypeC,
			GameMatcher.GearTypeCState, GameMatcher.Rotation
		));
		groups = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PuzzleGroup, GameMatcher.PuzzleInputs, GameMatcher.PuzzleTargetRange
		));
	}

	public void Execute() {
		foreach (var group in groups.GetEntities()) {
			var groupElements = gears.where(e => group.puzzleInputs.value.Contains(e.id.value)).ToList();
			var canBeLocked = group.puzzleTargetRange.value
				.Select(range => (x: range.x.positiveMod(360), y: range.y.positiveMod(360)))
				.Any(range => groupElements
					.Select(e => (state: e.gearTypeCState.value, rotation: e.rotation.value.positiveMod(360)))
					.All(gear => range.x < gear.rotation && range.y > gear.rotation)
				);

			if (canBeLocked && !temp) {
				temp = true;
				Debug.Log($"canBeLocked");
				// if (!groupElements.Any(e => e.gearTypeCState.value.isClosed())) {
					foreach (var gear in groupElements) {
						Debug.Log(gear.rotation.value);
					}
				// }

				foreach (var gear in groupElements) {
					// gear.isPuzzleElementDone = canBeLocked;
					gear.isGearTypeCLocked = true;
				}
			}
		}
	}
}