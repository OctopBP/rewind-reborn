using Entitas;
using Rewind.SharedData;
using Rewind.Services;
using UnityEngine;
using static LanguageExt.Prelude;

public class CharacterLookDirectionSystem : IExecuteSystem {
	readonly IGroup<GameEntity> players;
	readonly GameEntity clock;

	public CharacterLookDirectionSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Character, GameMatcher.CurrentPoint)
		);
	}

	public void Execute() {
		var isRewind = clock.clockState.value.isRewind();
		foreach (var player in players.GetEntities()) {
			var maybePreviousPoint = player.maybeValue(p => p.hasPreviousPoint, p => p.previousPoint.value);
			maybePreviousPoint.IfSome(previousPoint => {
				var samePath = player.currentPoint.value.pathId == previousPoint.pathId;
				var indexDiff = samePath ? player.currentPoint.value.index - previousPoint.index : 0;
				var maybeNewDirection = indexDiff switch {
					< 0 => Some(isRewind ? CharacterLookDirection.Right : CharacterLookDirection.Left),
					> 0 => Some(isRewind ? CharacterLookDirection.Left : CharacterLookDirection.Right),
					_ => None,
				};
				
				var maybeCurrentDirection = player.maybeValue(
					_ => _.hasCharacterLookDirection, _ => _.characterLookDirection.value
				);

				if (maybeNewDirection != maybeCurrentDirection) {
					maybeNewDirection.IfSome(newDirection => player.ReplaceCharacterLookDirection(newDirection));
				}
			});
		}
	}
}