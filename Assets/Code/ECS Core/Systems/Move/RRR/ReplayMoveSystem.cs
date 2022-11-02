using System.Linq;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

public class ReplayMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> clones;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public ReplayMoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		clones = contexts.game.GetGroup(
			GameMatcher.Clone
		);
		timePoints = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Timestamp, GameMatcher.CurrentPoint, GameMatcher.PreviousPoint)
		);
	}

	public void Execute() {
		if (!clock.clockState.value.isReplay()) return;
		
		foreach (var clone in clones.GetEntities()) {
			timePoints
				.GetEntities()
				.Where(p => p.timestamp.value < clock.time.value)
				.OrderBy(tp => tp.timestamp.value)
				.first()
				.IfSome(timePoint => useTimePoint(clone, timePoint));
		}

		void useTimePoint(GameEntity clone, GameEntity timePoint) {
			clone.ReplaceCurrentPoint(timePoint.currentPoint.value);
			clone.ReplacePreviousPoint(timePoint.previousPoint.value);

			timePoint.Destroy();
		}
	}
}