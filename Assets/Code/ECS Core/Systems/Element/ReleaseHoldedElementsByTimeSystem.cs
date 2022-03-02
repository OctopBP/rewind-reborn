using Entitas;

public class ReleaseHoldedElementsByTimeSystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly GameEntity settings;
	readonly IGroup<GameEntity> elements;

	public ReleaseHoldedElementsByTimeSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		settings = contexts.game.gameSettingsEntity;
		elements = contexts.game.GetGroup(GameMatcher.HoldedAtTime);
	}

	public void Execute() {
		var rrrCycleTime = settings.gameSettings.value.rewindTime * 3;
		
		foreach (var element in elements.GetEntities()) {
			if (element.holdedAtTime.value + rrrCycleTime < clock.time.value) {
				element.RemoveHoldedAtTime();
			}
		}
	}
}