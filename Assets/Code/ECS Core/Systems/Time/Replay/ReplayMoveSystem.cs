using Entitas;
using Rewind.ECSCore.Enums;

public class ReplayMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> clones;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public ReplayMoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;

		clones = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Clone, GameMatcher.Movable,
			GameMatcher.PointIndex, GameMatcher.PreviousPointIndex
		));

		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.TimePoint, GameMatcher.PointIndex, GameMatcher.PreviousPointIndex,
			GameMatcher.PathIndex, GameMatcher.PreviousPathIndex
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isReplay()) return;

		foreach (var clone in clones.GetEntities()) {
			foreach (var timePoint in timePoints.GetEntities()) {
				if (timePoint.timePoint.value >= clock.time.value) continue;

				clone.ReplacePointIndex(timePoint.pointIndex.value);
				clone.ReplacePreviousPointIndex(timePoint.previousPointIndex.value);
				clone.ReplacePathIndex(timePoint.pathIndex.value);
				clone.ReplacePreviousPathIndex(timePoint.previousPathIndex.value);

				timePoint.Destroy();
			}
		}
	}
}