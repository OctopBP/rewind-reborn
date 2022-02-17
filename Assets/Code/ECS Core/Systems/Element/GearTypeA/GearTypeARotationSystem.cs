using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

public class GearTypeARotationSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> gears;

	public GearTypeARotationSystem(Contexts contexts) {
		game = contexts.game;
		gears = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeA, GameMatcher.GearTypeAState, GameMatcher.Rotation
		));
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) {
			var speed = gear.gearTypeAData.value.rotateSpeed * gear.gearTypeAState.value.speedMultiplier();
			var newRotation = gear.rotation.value + speed * game.time.value.deltaTime;
			var limitedRotation = newRotation.clamp(0, gear.gearTypeAData.value.rotateLimit);
			gear.ReplaceRotation(limitedRotation);
		}
	}
}