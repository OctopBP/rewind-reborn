using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

/// <summary>
/// Switch time state states, when timer ends
/// <br/>Rewind -> Replay
/// <br/>Replay -> Record
/// </summary>
public class TimeStateSystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly ConfigEntity settings;
	readonly IGroup<GameEntity> timePoints;

	public TimeStateSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		settings = contexts.config.gameSettingsEntity;
		timePoints = contexts.game.GetGroup(GameMatcher.Timestamp);
	}

	public void Execute() {
		if (!clock.isTimerComplete) return;

		clock.clockState.value.fold(
			onRecord: default,
			onRewind: () => {
				clock.ReplaceClockState(ClockState.Replay);
				clock.ReplaceTimer(settings.gameSettings.value.rewindTime);
				clock.with(x => x.isTimerComplete = false);
			},
			onReplay: () => {
				clock.ReplaceClockState(ClockState.Record);
				foreach (var timePoint in timePoints.GetEntities()) {
					timePoint.Destroy();
				}
			}
		);
	}
}