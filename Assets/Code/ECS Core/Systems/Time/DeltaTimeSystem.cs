using Entitas;
using Rewind.ECSCore.Enums;

public class DeltaTimeSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> clocks;
	bool firstExecute;

	public DeltaTimeSystem(Contexts contexts) {
		game = contexts.game;
		clocks = game.GetGroup(GameMatcher.AllOf(GameMatcher.Clock, GameMatcher.ClockData, GameMatcher.ClockState));
		firstExecute = true;
	}

	public void Execute() {
		foreach (var clock in clocks.GetEntities()) {
			// We need skip first Execute to sync clock.time with Time.time
			var delta = firstExecute ? 0 : game.worldTime.value.deltaTime;
			firstExecute = false;

			var timeSpeed = clock.clockState.value.map(
				onRecord: clock.clockData.value.normalSpeed,
				onRewind: clock.clockData.value.rewindSpeed,
				onReplay: clock.clockData.value.normalSpeed
			);
			
			clock.ReplaceDeltaTime(delta * timeSpeed);
		}
	}
}