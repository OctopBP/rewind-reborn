using System.Linq;
using Entitas;
using Rewind.Extensions;
using Rewind.Services;
using UnityEngine;

public class CheckPuzzleRotationIsDoneSystem : IExecuteSystem {
	readonly IGroup<GameEntity> elements;
	readonly IGroup<GameEntity> groups;
	bool temp;

	public CheckPuzzleRotationIsDoneSystem(Contexts contexts) {
		elements = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Id, GameMatcher.PuzzleElement, GameMatcher.Rotation
		));
		groups = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PuzzleGroup, GameMatcher.PuzzleInputs, GameMatcher.PuzzleTargetRange
		));
	}

	public void Execute() {
		foreach (var group in groups.GetEntities()) {
			var groupElements = elements.where(e => group.puzzleInputs.value.Contains(e.id.value)).ToList();
			var canBeLocked = group.puzzleTargetRange.value
				.Select(range => (x: range.x.positiveMod(360), y: range.y.positiveMod(360)))
				.Any(range => groupElements
					.Select(e => e.rotation.value.positiveMod(360))
					.All(rotation => range.x < rotation && range.y > rotation)
				);

			if (canBeLocked && !temp) {
				temp = true;
				Debug.Log($"canBeLocked");
				// if (!groupElements.Any(e => e.gearTypeCState.value.isClosed())) {
					foreach (var gear in groupElements) {
						Debug.Log(gear.rotation.value);
					}
				// }

				group.isPuzzleComplete = true;

				foreach (var gear in groupElements) {
					// gear.isPuzzleElementDone = canBeLocked;
					gear.isGearTypeCLocked = true;
				}
			}
		}
	}
}