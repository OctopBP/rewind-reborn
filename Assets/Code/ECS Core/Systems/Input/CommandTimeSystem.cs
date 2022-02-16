using Entitas;
using Rewind.ECSCore.Enums;

public class CommandTimeSystem : IExecuteSystem {
	readonly InputContext input;
	readonly GameEntity clock;

	public CommandTimeSystem(Contexts contexts) {
		input = contexts.input;
		clock = contexts.game.clockEntity;
	}

	public void Execute() {
		if (!input.input.value.getRewindButtonDown()) return;
		if (!clock.clockState.value.isRecord()) return;

		clock.ReplaceClockState(ClockState.Rewind);
		// clock.ReplaceTimer(game.settings.Value.RewindTime);
		clock.ReplaceTimer(5); // todo:
	}
}