using Entitas;
using Rewind.ECSCore.Enums;

public class TimeStateSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> timePoints;

	public TimeStateSystem(Contexts contexts) {
		game = contexts.game;
		timePoints = game.GetGroup(GameMatcher.TimePoint);
	}

	public void Execute() {
		var clock = game.clockEntity;
		if (!clock.isTimerComplete) return;

		switch (clock.clockState.value) {
			case ClockState.Rewind:
				clock.ReplaceClockState(ClockState.Replay);
				clock.isTimerComplete = false;
				// clock.ReplaceTimer(_game.settings.Value.RewindTime);
				clock.ReplaceTimer(5);
				break;

			case ClockState.Replay: {
				clock.ReplaceClockState(ClockState.Record);
				foreach (var timePoint in timePoints.GetEntities()) timePoint.Destroy();
				break;
			}
		}
	}
}