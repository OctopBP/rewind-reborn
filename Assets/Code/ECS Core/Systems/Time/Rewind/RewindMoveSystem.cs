using Entitas;

public class RewindMoveSystem : IExecuteSystem
{
	private readonly GameContext _game;
	private readonly IGroup<GameEntity> _movers;
	private readonly IGroup<GameEntity> _timePoints;

	public RewindMoveSystem(Contexts contexts)
	{
		_game = contexts.game;

		_movers = contexts.game
			.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Mover,
					GameMatcher.PointIndex,
					GameMatcher.PreviousPointIndex));

		_timePoints = contexts.game
			.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.TimePoint,
					GameMatcher.PointIndex,
					GameMatcher.PreviousPointIndex,
					GameMatcher.PathIndex,
					GameMatcher.PreviousPathIndex,
					GameMatcher.RewindPointIndex));
	}

	public void Execute()
	{
		GameEntity clock = _game.clockEntity;
		if (!clock.isRewind) return;

		foreach (GameEntity mover in _movers.GetEntities())
		{
			foreach (GameEntity timePoint in _timePoints.GetEntities())
			{
				if (timePoint.timePoint.Value != clock.timeTick.Value) continue;

				mover.ReplacePointIndex(timePoint.rewindPointIndex.Value);
				mover.ReplacePreviousPointIndex(timePoint.pointIndex.Value);
				mover.ReplacePathIndex(timePoint.previousPathIndex.Value);
				mover.ReplacePreviousPathIndex(timePoint.pathIndex.Value);

				_game.logger.Value.LogMessage($"[{_game.clockEntity.timeTick.Value}] RewindMove pointIndex:{timePoint.pointIndex.Value} previousPointIndex:{timePoint.previousPointIndex.Value}");

				// timePoint.Destroy();
			}
		}
	}
}
