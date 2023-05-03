using Entitas;
using Rewind.SharedData;

public class CommandTimeSystem : IExecuteSystem {
	readonly InputContext input;
	readonly GameEntity clock;
	readonly ConfigEntity settings;

	public CommandTimeSystem(Contexts contexts) {
		input = contexts.input;
		clock = contexts.game.clockEntity;
		settings = contexts.config.gameSettingsEntity;
	}

	public void Execute() {
		if (!input.input.value.getRewindButtonDown()) return;
		if (!clock.clockState.value.isRecord()) return;

		clock.ReplaceClockState(ClockState.Rewind);
		clock.ReplaceTimer(settings.gameSettings.value._rewindTime);
	}
}