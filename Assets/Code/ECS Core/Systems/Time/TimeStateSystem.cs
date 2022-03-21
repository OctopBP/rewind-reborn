using Code.Helpers.InspectorGraphs;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using UnityEngine;

/// <summary>
/// Switch time state states, when timer ends
/// <br/>Rewind -> Replay
/// <br/>Replay -> Record
/// </summary>
public class TimeStateSystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly GameEntity settings;
	readonly IGroup<GameEntity> timePoints;

	public TimeStateSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		settings = contexts.game.gameSettingsEntity;
		timePoints = contexts.game.GetGroup(GameMatcher.TimePoint);
	}

	public void Execute() {
		if (!clock.isTimerComplete) return;

		if (clock.clockState.value == ClockState.Rewind) {
			clock.ReplaceClockState(ClockState.Replay);
			GraphBehaviour.init.timeLines.Add(new(Time.time, GraphBehaviour.Init.TimeLine.Type.Replay));
			clock.ReplaceTimer(settings.gameSettings.value.rewindTime);
			clock.with(x => x.isTimerComplete = false);
		} else if (clock.clockState.value == ClockState.Replay) {
			clock.ReplaceClockState(ClockState.Record);
			GraphBehaviour.init.timeLines.Add(new(Time.time, GraphBehaviour.Init.TimeLine.Type.Record));
			foreach (var timePoint in timePoints.GetEntities())
				timePoint.Destroy();
		}
	}
}