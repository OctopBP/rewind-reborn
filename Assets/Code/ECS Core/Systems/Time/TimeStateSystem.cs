using Entitas;
using Rewind.SharedData;

/// <summary>
/// Switch time state states, when timer ends
/// <br/>Rewind -> Replay
/// <br/>Replay -> Record
/// </summary>
public class TimeStateSystem : IExecuteSystem
{
	private readonly GameEntity clock;
	private readonly ConfigEntity settings;
	private readonly IGroup<GameEntity> timePoints;

	public TimeStateSystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		settings = contexts.config.gameSettingsEntity;
		timePoints = contexts.game.GetGroup(GameMatcher.Timestamp);
	}

	public void Execute()
	{
		if (!clock.isTimerComplete) return;

		clock.clockState.value.Fold(
			onRecord: default,
			onRewind: () => clock
				.ReplaceClockState(ClockState.Replay)
				.ReplaceTimer(settings.gameSettings.value._rewindTime)
				.SetTimerComplete(false),
			onReplay: () =>
			{
				clock.ReplaceClockState(ClockState.Record);
				foreach (var timePoint in timePoints.GetEntities())
				{
					timePoint.Destroy();
				}
			}
		);
	}
}