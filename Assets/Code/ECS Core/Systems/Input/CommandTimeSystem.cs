using Code.Helpers.InspectorGraphs;
using Entitas;
using Rewind.ECSCore.Enums;
using UnityEngine;

public class CommandTimeSystem : IExecuteSystem {
	readonly InputContext input;
	readonly GameEntity clock;
	readonly GameEntity settings;

	public CommandTimeSystem(Contexts contexts) {
		input = contexts.input;
		clock = contexts.game.clockEntity;
		settings = contexts.game.gameSettingsEntity;
	}

	public void Execute() {
		if (!input.input.value.getRewindButtonDown()) return;
		if (!clock.clockState.value.isRecord()) return;
		
		GraphBehaviour.init?.timeLines.Add(new(Time.time - settings.gameSettings.value.rewindTime,
			GraphBehaviour.Init.TimeLine.Type.StartRecord));
		GraphBehaviour.init?.timeLines.Add(new(Time.time, GraphBehaviour.Init.TimeLine.Type.Rewind));

		clock.ReplaceClockState(ClockState.Rewind);
		clock.ReplaceTimer(settings.gameSettings.value.rewindTime);
	}
}