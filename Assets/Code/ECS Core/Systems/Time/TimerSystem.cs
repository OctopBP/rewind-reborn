using Entitas;

public class TimerSystem : IExecuteSystem, ICleanupSystem
{
	private readonly IGroup<GameEntity> timers;
	private readonly IGroup<GameEntity> timerCompletes;
	private readonly GameEntity clock;

	public TimerSystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		timers = contexts.game.GetGroup(GameMatcher.Timer);
		timerCompletes = contexts.game.GetGroup(GameMatcher.TimerComplete);
	}

	public void Execute()
	{
		foreach (var timer in timers.GetEntities())
        {
			if (timer.timer.value > 0)
            {
				timer.ReplaceTimer(timer.timer.value - clock.deltaTime.value);
			}
			else
            {
				timer.SetTimerComplete(true);
			}
		}
	}

	public void Cleanup()
    {
		foreach (var timerComplete in timerCompletes.GetEntities())
        {
			timerComplete.RemoveTimer();
			timerComplete.SetTimerComplete(false);
		}
	}
}