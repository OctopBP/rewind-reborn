using Entitas;
using Rewind.Services;

public class CheckFinishSystem : IExecuteSystem {
	readonly IGroup<GameEntity> finishes;
	readonly IGroup<GameEntity> players;

	public CheckFinishSystem(Contexts contexts) {
		finishes = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Finish, GameMatcher.CurrentPoint
		));
		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.CurrentPoint
		));
	}

	public void Execute() {
		foreach (var finish in finishes.GetEntities()) {
			foreach (var player in players.GetEntities()) {
				if (finish.isSamePoint(player)) {
					finish.isFinishReached = true;
				}
			}
		}
	}
}