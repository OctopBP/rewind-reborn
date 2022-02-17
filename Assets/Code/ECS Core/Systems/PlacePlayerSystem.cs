using Entitas;
using Rewind.Services;

public class PlacePlayerSystem : IInitializeSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> players;

	public PlacePlayerSystem(Contexts contexts) {
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PathIndex, GameMatcher.PointIndex, GameMatcher.Position
		));

		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.PathIndex, GameMatcher.PointIndex, GameMatcher.Position
		));
	}

	public void Initialize() {
		foreach (var player in players.GetEntities()) {
			points.first(player.isSamePoint).IfSome(point => player.ReplacePosition(point.position.value));
		}
	}
}