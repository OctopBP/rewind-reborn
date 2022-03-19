using Entitas;
using Rewind.ECSCore.Enums;

public class TimeSystem : IExecuteSystem {
	readonly GameContext game;
	readonly GameEntity clock;
	bool firstExecute;

	public TimeSystem(Contexts contexts) {
		game = contexts.game;
		clock = game.clockEntity;
		firstExecute = true;
	}

	public void Execute() {
		var currentTime = clock.time.value;

		// We need skip first Execute to sync clock.time with Time.time
		var delta = firstExecute ? 0 : game.worldTime.value.deltaTime * clock.clockState.value.timeDirection();
		firstExecute = false;

		clock.ReplaceTime(currentTime + delta);
	}
}