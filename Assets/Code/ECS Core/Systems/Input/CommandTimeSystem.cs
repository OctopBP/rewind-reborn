using Entitas;
using Rewind.ECSCore.Enums;

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

		clock.ReplaceClockState(ClockState.Rewind);
		clock.ReplaceTimer(settings.gameSettings.value.rewindTime);
	}
}