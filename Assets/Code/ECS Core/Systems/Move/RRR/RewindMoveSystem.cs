using System.Linq;
using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class RewindMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public RewindMoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(
			GameMatcher.Player
		);
		timePoints = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Timestamp, GameMatcher.CurrentPoint, GameMatcher.PreviousPoint, GameMatcher.RewindPoint)
			.NoneOf(GameMatcher.TimePointUsed)
		);
	}

	public void Execute() {
		if (!clock.clockState.value.isRewind()) return;

		foreach (var player in players.GetEntities()) {
			timePoints
				.GetEntities()
				.Where(p => p.timestamp.value >= clock.time.value)
				.OrderByDescending(tp => tp.timestamp.value)
				.forEach(timePoint => useTimePoint(player, timePoint));
		}

		void useTimePoint(GameEntity player, GameEntity timePoint) {
			timePoint.SetTimePointUsed(true);
			player
				.ReplaceCurrentPoint(timePoint.rewindPoint.value)
				.ReplacePreviousPoint(timePoint.previousPoint.value);
		}
	}
}