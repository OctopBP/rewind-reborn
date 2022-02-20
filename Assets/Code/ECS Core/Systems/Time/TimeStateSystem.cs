using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

public class TimeStateSystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly IGroup<GameEntity> timePoints;

	public TimeStateSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		timePoints = contexts.game.GetGroup(GameMatcher.TimePoint);
	}

	public void Execute() {
		if (!clock.isTimerComplete) return;

		if (clock.clockState.value == ClockState.Rewind) {
			clock.ReplaceClockState(ClockState.Replay);
			// clock.ReplaceTimer(_game.settings.Value.RewindTime);
			clock.ReplaceTimer(5);
			clock.with(x => x.isTimerComplete = false);
		} else if (clock.clockState.value == ClockState.Replay) {
			clock.ReplaceClockState(ClockState.Record);
			foreach (var timePoint in timePoints.GetEntities())
				timePoint.Destroy();
		}
	}
}