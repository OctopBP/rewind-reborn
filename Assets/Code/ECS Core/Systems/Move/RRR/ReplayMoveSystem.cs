using System.Linq;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class ReplayMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> clones;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public ReplayMoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		clones = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Clone, GameMatcher.Movable, GameMatcher.PointIndex, GameMatcher.PreviousPointIndex
		));
		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.TimePoint, GameMatcher.PointIndex, GameMatcher.PreviousPointIndex
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isReplay()) return;
		
		var futureTimePoints = timePoints.where(tp => tp.timePoint.value >= clock.time.value).ToArray();

		foreach (var clone in clones.GetEntities()) {
			foreach (var timePoint in futureTimePoints) {
				clone.ReplacePointIndex(timePoint.pointIndex.value);
				clone.ReplacePreviousPointIndex(timePoint.previousPointIndex.value);

				timePoint.Destroy();
			}
		}
	}
}