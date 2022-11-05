using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

public class GearTypeARotationSystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly IGroup<GameEntity> gears;

	public GearTypeARotationSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		gears = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.GearTypeA, GameMatcher.GearTypeAState, GameMatcher.GearTypeAData, GameMatcher.Rotation)
		);
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) {
			var speed = gear.gearTypeAData.value.rotateSpeed * gear.gearTypeAState.value.speedMultiplier();
			var newRotation = gear.rotation.value + speed * clock.deltaTime.value;
			var limitedRotation = newRotation.clamp(0, gear.gearTypeAData.value.rotateLimit);
			gear.ReplaceRotation(limitedRotation);
		}
	}
}