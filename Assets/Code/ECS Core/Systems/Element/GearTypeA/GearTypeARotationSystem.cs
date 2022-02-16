using Entitas;

public class GearTypeARotationSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> gears;

	public GearTypeARotationSystem(Contexts contexts) {
		game = contexts.game;
		gears = game.GetGroup(GameMatcher.AllOf(GameMatcher.GearTypeA, GameMatcher.Rotation));
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) {
			gear.ReplaceRotation(gear.rotation.value + game.time.value.deltaTime * 10);
		}
	}
}