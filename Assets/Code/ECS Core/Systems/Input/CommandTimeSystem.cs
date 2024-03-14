using Entitas;
using Rewind.SharedData;

public class CommandTimeSystem : IExecuteSystem
{
	private readonly InputContext input;
	private readonly GameEntity clock;
	private readonly ConfigEntity settings;

	public CommandTimeSystem(Contexts contexts)
	{
		input = contexts.input;
		clock = contexts.game.clockEntity;
		settings = contexts.config.gameSettingsEntity;
	}

	public void Execute()
	{
		if (!input.input.value.GetRewindButtonDown()) return;
		if (!clock.clockState.value.IsRecord()) return;

		clock.ReplaceClockState(ClockState.Rewind);
		clock.ReplaceTimer(settings.gameSettings.value._rewindTime);
	}
}