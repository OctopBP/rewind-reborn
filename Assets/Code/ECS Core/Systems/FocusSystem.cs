using Entitas;
using Rewind.Services;

public class FocusSystem : IExecuteSystem {
	readonly IGroup<GameEntity> focusables;
	readonly IGroup<GameEntity> players;

	public FocusSystem(Contexts contexts) {
		focusables = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Focusable, GameMatcher.PathIndex, GameMatcher.PointIndex
		));

		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Character, GameMatcher.PathIndex,
			GameMatcher.PointIndex, GameMatcher.PreviousPointIndex
		));
	}

	public void Execute() {
		foreach (var focusable in focusables.GetEntities()) {
			focusable.isFocus = players.any(p => focusable.onPoint(p) && p.isMoveComplete);
		}
	}
}