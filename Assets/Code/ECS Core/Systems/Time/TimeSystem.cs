using Entitas;
using Rewind.ECSCore.Enums;

public class TimeSystem : IExecuteSystem {
	readonly GameEntity clock;

	public TimeSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
	}

	public void Execute() {
		var currentTick = clock.tick.value;
		clock.ReplaceTick(currentTick + clock.clockState.value.timeDirection());
	}
}