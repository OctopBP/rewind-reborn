using Entitas;
using Rewind.Services;

public class FocusSystem : IExecuteSystem {
	readonly IGroup<GameEntity> focusables;
	readonly IGroup<GameEntity> players;

	public FocusSystem(Contexts contexts) {
		focusables = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Focusable, GameMatcher.CurrentPoint
		));
		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.CurrentPoint
		));
	}

	public void Execute() {
		foreach (var focusable in focusables.GetEntities()) {
			focusable.isFocus = players.any(p => playerOnPoint(focusable, p) && p.isMoveComplete);
		}

		bool playerOnPoint(GameEntity point, GameEntity player) =>
			point.currentPoint.value == player.currentPoint.value &&
			!player.hasPreviousPoint;
	}
}