using Entitas;
using Rewind.ECSCore.Enums;

public class CharacterStateSystem : IExecuteSystem {
	readonly IGroup<GameEntity> characters;

	public CharacterStateSystem(Contexts contexts) {
		characters = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Character, GameMatcher.MoveComplete)
		);
	}

	public void Execute() {
		foreach (var character in characters.GetEntities()) {
			var state = character.isMoveComplete() ? CharacterState.Idle : CharacterState.Walk;
			if (character.characterState.value != state) {
				character.ReplaceCharacterState(state);
			}
		}
	}
}
