using Entitas;

public class TimeSystem : IInitializeSystem, IExecuteSystem {
	readonly GameContext game;
	GameEntity clock;

	public TimeSystem(Contexts contexts) {
		game = contexts.game;
	}

	public void Initialize() {
		game.isClock = true;
		clock = game.clockEntity;
		clock.AddTick(0);
	}

	public void Execute() {
		var currentTick = clock.tick.value;
		clock.ReplaceTick(currentTick + 1);
		
		// if (clock.isRewind)
		// 	clock.ReplaceTimeTick(currentTick - 1);
		// else
		// 	clock.ReplaceTimeTick(currentTick + 1);
	}
}