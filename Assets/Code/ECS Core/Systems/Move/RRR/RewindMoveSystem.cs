using System.Linq;
using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class RewindMoveSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> players;
	private readonly IGroup<GameEntity> timePoints;
	private readonly GameEntity clock;

	public RewindMoveSystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(
			GameMatcher.Player
		);
		timePoints = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Timestamp, GameMatcher.CurrentPoint, GameMatcher.PreviousPoint, GameMatcher.RewindPoint)
			.NoneOf(GameMatcher.TimePointUsed)
		);
	}

	public void Execute()
	{
		if (!clock.clockState.value.IsRewind()) return;

		foreach (var player in players.GetEntities())
		{
			timePoints
				.GetEntities()
				.Where(p => p.timestamp.value >= clock.time.value)
				.OrderByDescending(tp => tp.timestamp.value)
				.ForEach(timePoint => UseTimePoint(player, timePoint));
		}

		void UseTimePoint(GameEntity player, GameEntity timePoint)
		{
			timePoint.SetTimePointUsed(true);
			player
				.ReplaceCurrentPoint(timePoint.rewindPoint.value)
				.ReplacePreviousPoint(timePoint.previousPoint.value);
		}
	}
}