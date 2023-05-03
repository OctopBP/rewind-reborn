using Entitas;
using Rewind.Services;

public class PlaceCharactersSystem : IInitializeSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> characters;
	readonly ConfigEntity gameSettings;

	public PlaceCharactersSystem(Contexts contexts) {
		points = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.Position)
		);
		characters = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Character, GameMatcher.CurrentPoint, GameMatcher.Position)
		);
		gameSettings = contexts.config.gameSettingsEntity;
	}

	public void Initialize() {
		foreach (var character in characters.GetEntities()) {
			// points.first(character.isSamePoint).IfSome(point => character.ReplacePosition(point.position.value));
			// character.AddPathFollowerSpeed(gameSettings.gameSettings.value.characterSpeed);
		}
	}
}