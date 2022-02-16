using Entitas;

public class TimerSystem : IExecuteSystem, ICleanupSystem
{
	private readonly IGroup<GameEntity> _timers;
	private readonly IGroup<GameEntity> _timerCompletes;
	private readonly GameContext _game;

	public TimerSystem(Contexts contexts)
	{
		_game = contexts.game;
		_timers = _game.GetGroup(GameMatcher.Timer);
		_timerCompletes = _game.GetGroup(GameMatcher.TimerComplete);
	}

	public void Execute()
	{
		foreach (GameEntity timer in _timers.GetEntities())
		{
			if (timer.timer.Value > 0)
				timer.ReplaceTimer(timer.timer.Value - _game.time.Value.DeltaTime);
			else
				timer.isTimerComplete = true;
		}
	}

	public void Cleanup()
	{
		foreach (GameEntity timerComplete in _timerCompletes.GetEntities())
		{
			timerComplete.RemoveTimer();
			timerComplete.isTimerComplete = false;
		}
	}
}
