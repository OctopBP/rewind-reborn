using Entitas;
using Rewind.ECSCore.Enums;

public class RewindMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public RewindMoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;

		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.PointIndex, GameMatcher.PreviousPointIndex
		));

		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.TimePoint, GameMatcher.PointIndex, GameMatcher.PreviousPointIndex,
			GameMatcher.PathIndex, GameMatcher.PreviousPathIndex, GameMatcher.RewindPointIndex
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isRewind()) return;

		foreach (var player in players.GetEntities()) {
			foreach (var timePoint in timePoints.GetEntities()) {
				if (timePoint.timePoint.value != clock.tick.value) continue;

				player.ReplacePreviousPointIndex(timePoint.pointIndex.value);
				player.ReplacePointIndex(timePoint.rewindPointIndex.value);
				player.ReplacePathIndex(timePoint.previousPathIndex.value);
				player.ReplacePreviousPathIndex(timePoint.pathIndex.value);

				// timePoint.Destroy();
			}
		}
	}
}