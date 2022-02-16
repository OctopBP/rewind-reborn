using Entitas;
using Rewind.ECSCore.Enums;

public class RewindMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> movers;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public RewindMoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;

		movers = contexts.game.GetGroup(GameMatcher.AllOf(
			// GameMatcher.Mover,
			GameMatcher.PointIndex, GameMatcher.PreviousPointIndex
		));

		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.TimePoint, GameMatcher.PointIndex, GameMatcher.PreviousPointIndex,
			GameMatcher.PathIndex
			// GameMatcher.PreviousPathIndex, GameMatcher.RewindPointIndex
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isRewind()) return;

		foreach (var mover in movers.GetEntities()) {
			foreach (var timePoint in timePoints.GetEntities()) {
				if (timePoint.timePoint.value != clock.tick.value) continue;

				// mover.ReplacePointIndex(timePoint.rewindPointIndex.value);
				mover.ReplacePreviousPointIndex(timePoint.pointIndex.value);
				// mover.ReplacePathIndex(timePoint.previousPathIndex.value);
				// mover.ReplacePreviousPathIndex(timePoint.pathIndex.value);

				// timePoint.Destroy();
			}
		}
	}
}