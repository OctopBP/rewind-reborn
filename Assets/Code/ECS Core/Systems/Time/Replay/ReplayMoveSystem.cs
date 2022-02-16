using Entitas;

public class ReplayMoveSystem : IExecuteSystem
{
	private readonly GameContext _game;
	private readonly IGroup<GameEntity> _clones;
	private readonly IGroup<GameEntity> _timePoints;

	public ReplayMoveSystem(Contexts contexts)
	{
		_game = contexts.game;

		_clones = contexts.game
			.GetGroup(GameMatcher
				.AllOf(
					GameMatcher.Clone,
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
					GameMatcher.PreviousPathIndex));
	}

	public void Execute()
	{
		GameEntity clock = _game.clockEntity;
		if (!clock.isReplay) return;

		foreach (GameEntity clone in _clones.GetEntities())
		{
			foreach (GameEntity timePoint in _timePoints.GetEntities())
			{
				if (timePoint.timePoint.Value != clock.timeTick.Value) continue;

				_game.logger.Value.LogMessage($"[{_game.clockEntity.timeTick.Value}] ReplayMove pointIndex:{timePoint.pointIndex.Value} previousPointIndex:{timePoint.previousPointIndex.Value}");

				clone.ReplacePointIndex(timePoint.pointIndex.Value);
				clone.ReplacePreviousPointIndex(timePoint.previousPointIndex.Value);
				clone.ReplacePathIndex(timePoint.pathIndex.Value);
				clone.ReplacePreviousPathIndex(timePoint.previousPathIndex.Value);
				timePoint.Destroy();
			}
		}
	}
}
