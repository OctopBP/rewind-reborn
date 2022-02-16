using Entitas;

public class TimeStateSystem : IExecuteSystem
{
	private readonly GameContext _game;
	private readonly IGroup<GameEntity> _timePoints;

	public TimeStateSystem(Contexts contexts)
	{
		_game = contexts.game;
		_timePoints = contexts.game.GetGroup(GameMatcher.TimePoint);
	}

	public void Execute()
	{
		GameEntity clock = _game.clockEntity;

		if (!clock.isTimerComplete) return;

		if (clock.isRewind)
		{
			clock.isRewind = false;
			clock.isReplay = true;

			_game.logger.Value.LogMessage($"[{_game.clockEntity.timeTick.Value}] -> Replay");

			clock.isTimerComplete = false;
			clock.ReplaceTimer(_game.settings.Value.RewindTime);

			return;
		}

		if (clock.isReplay)
		{
			clock.isReplay = false;

			_game.logger.Value.LogMessage($"[{_game.clockEntity.timeTick.Value}] -> Record");

			foreach (GameEntity timePoint in _timePoints.GetEntities())
			{
				timePoint.Destroy();
			}
		}
	}
}
