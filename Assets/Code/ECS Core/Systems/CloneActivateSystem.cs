using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

public class CloneActivateSystem : IExecuteSystem {
	readonly IGroup<GameEntity> clones;
	readonly IGroup<GameEntity> players;
	readonly GameEntity clock;

	public CloneActivateSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;

		clones = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Clone, GameMatcher.View));

		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.PathIndex,
			GameMatcher.PointIndex, GameMatcher.Position
		));
	}

	public void Execute() {
		foreach (var clone in clones.GetEntities()) {
			var viewDisabled = clone.isViewDisabled;
			var needUpdate = viewDisabled == clock.clockState.value.isReplay();

			if (needUpdate && players.first().valueOut(out var player)) {
				clone.ReplacePosition(player.position.value);
				clone.ReplacePathIndex(player.pathIndex.value);
				clone.ReplacePointIndex(player.pointIndex.value);
				clone.isViewDisabled = !viewDisabled;
			}
		}
	}
}