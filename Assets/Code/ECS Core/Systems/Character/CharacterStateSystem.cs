using Entitas;
using Rewind.ECSCore.Enums;

public class CharacterStateSystem : IExecuteSystem {
	readonly IGroup<GameEntity> players;

	public CharacterStateSystem(Contexts contexts) {
		players = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Player, GameMatcher.Position)
		);
	}

	public void Execute() {
		foreach (var player in players.GetEntities()) {
			var state = player.isMoveComplete ? CharacterState.Idle : CharacterState.Walk;
			if (player.characterState.value != state) {
				player.ReplaceCharacterState(state);
			}
		}
	}
}
