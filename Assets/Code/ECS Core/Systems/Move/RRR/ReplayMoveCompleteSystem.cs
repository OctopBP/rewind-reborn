using System.Linq;
using Entitas;
using Rewind.SharedData;
using Rewind.Extensions;

public class ReplayMoveCompleteSystem : IExecuteSystem {
	readonly IGroup<GameEntity> clones;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public ReplayMoveCompleteSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		clones = contexts.game.GetGroup(
			GameMatcher.Clone
		);
		timePoints = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Timestamp, GameMatcher.MoveComplete)
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
			clone.ReplaceMoveComplete(timePoint.isMoveComplete());
			timePoint.Destroy();
		}
	}
}