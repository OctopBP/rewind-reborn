using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

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
		).NoneOf(
			GameMatcher.TimePointUsed
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isRewind()) return;

		foreach (var player in players.GetEntities()) {
			timePoints.first(p => p.timePoint.value >= clock.time.value).IfSome(timePoint => {
				timePoint.with(x => x.isTimePointUsed = true);
				player.ReplacePreviousPointIndex(timePoint.pointIndex.value);
				player.ReplacePointIndex(timePoint.rewindPointIndex.value);
				player.ReplacePathIndex(timePoint.previousPathIndex.value);
				player.ReplacePreviousPathIndex(timePoint.pathIndex.value);
			});
		}
	}
}