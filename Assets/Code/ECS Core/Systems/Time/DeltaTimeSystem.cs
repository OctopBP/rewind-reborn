using Entitas;
using Rewind.ECSCore.Enums;

public class DeltaTimeSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> clocks;
	readonly GameSettingsComponent gameSettingsComponent;

	bool firstExecute;

	public DeltaTimeSystem(Contexts contexts) {
		game = contexts.game;
		gameSettingsComponent = contexts.config.gameSettings;
		clocks = game.GetGroup(GameMatcher.AllOf(GameMatcher.Clock, GameMatcher.ClockState));
		firstExecute = true;
	}

	public void Execute() {
		foreach (var clock in clocks.GetEntities()) {
			// We need skip first Execute to sync clock.time with Time.time
			var delta = firstExecute ? 0 : game.worldTime.value.deltaTime;
			firstExecute = false;

			var config = gameSettingsComponent.value;
			var timeSpeed = clock.clockState.value.fold(
				onRecord: config.clockNormalSpeed,
				onRewind: config.clockRewindSpeed,
				onReplay: config.clockNormalSpeed
			);
			
			clock.ReplaceDeltaTime(delta * timeSpeed);
		}
	}
}