using Entitas;

public class TimerSystem : IExecuteSystem, ICleanupSystem {
	readonly IGroup<GameEntity> timers;
	readonly IGroup<GameEntity> timerCompletes;
	readonly GameContext game;

	public TimerSystem(Contexts contexts) {
		game = contexts.game;
		timers = game.GetGroup(GameMatcher.Timer);
		timerCompletes = game.GetGroup(GameMatcher.TimerComplete);
	}

	public void Execute() {
		foreach (var timer in timers.GetEntities()) {
			if (timer.timer.value > 0) {
				timer.ReplaceTimer(timer.timer.value - game.worldTime.value.deltaTime);
			} else {
				timer.isTimerComplete = true;
			}
		}
	}

	public void Cleanup() {
		foreach (var timerComplete in timerCompletes.GetEntities()) {
			timerComplete.RemoveTimer();
			timerComplete.isTimerComplete = false;
		}
	}
}