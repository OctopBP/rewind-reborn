using System.Linq;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

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
				.OrderBy(tp => tp.timestamp.value)
				.first()
				.IfSome(timePoint => useTimePoint(player, timePoint));
		}

		void useTimePoint(GameEntity player, GameEntity timePoint) {
			timePoint.isTimePointUsed = true;
			player.ReplaceCurrentPoint(timePoint.rewindPoint.value);
			player.ReplacePreviousPoint(timePoint.previousPoint.value);
		}
	}
}