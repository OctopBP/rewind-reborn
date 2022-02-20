using Entitas;
using Rewind.ECSCore.Enums;

public class TimeSystem : IExecuteSystem {
	readonly GameContext game;
	readonly GameEntity clock;

	public TimeSystem(Contexts contexts) {
		game = contexts.game;
		clock = game.clockEntity;
	}

	public void Execute() {
		var currentTime = clock.time.value;
		var delta = game.worldTime.value.deltaTime * clock.clockState.value.timeDirection();
		clock.ReplaceTime(currentTime + delta);
	}
}