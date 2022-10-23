using Entitas;
using Rewind.Services;

public class PlacePlayerSystem : IInitializeSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> players;
	readonly ConfigEntity gameSettings;

	public PlacePlayerSystem(Contexts contexts) {
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.Position
		));
		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.PointIndex, GameMatcher.Position
		));
		gameSettings = contexts.config.gameSettingsEntity;
	}

	public void Initialize() {
		foreach (var player in players.GetEntities()) {
			points.first(player.isSamePoint).IfSome(point => player.ReplacePosition(point.position.value));
			player.AddPathFollowerSpeed(gameSettings.gameSettings.value.characterSpeed);
		}
	}
}