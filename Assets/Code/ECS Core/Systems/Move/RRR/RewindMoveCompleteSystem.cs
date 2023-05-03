using System.Linq;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class RewindMoveCompleteSystem : IExecuteSystem {
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public RewindMoveCompleteSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(
			GameMatcher.Player
		);
		timePoints = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Timestamp, GameMatcher.MoveComplete)
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
			player.ReplaceMoveComplete(!timePoint.isMoveComplete());
		}
	}
}