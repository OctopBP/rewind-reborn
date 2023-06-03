using Entitas;
using Rewind.Services;
using Rewind.SharedData;

public class CharacterStateSystem : IExecuteSystem {
	readonly IGroup<GameEntity> characters;
	readonly IGroup<GameEntity> ladderPoints;

	public CharacterStateSystem(Contexts contexts) {
		characters = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Character, GameMatcher.CurrentPoint, GameMatcher.MoveComplete)
		);
		ladderPoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.LadderStair, GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.Depth
		));
	}

	public void Execute() {
		foreach (var character in characters.GetEntities()) {
			var onTheLadder = ladderPoints.any(p => p.isSamePoint(character.currentPoint.value));
			var state = onTheLadder
				? CharacterState.Ladder
				: character.isMoveComplete()
					? CharacterState.Idle
					: CharacterState.Walk;
			
			if (character.characterState.value != state) {
				character.ReplaceCharacterState(state);
			}
		}
	}
}
