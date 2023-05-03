using Entitas;
using Rewind.SharedData;
using Rewind.Extensions;

public class GearTypeCRotationSystem : IExecuteSystem {
	readonly GameEntity clock;
	readonly IGroup<GameEntity> gears;

	public GearTypeCRotationSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeC, GameMatcher.GearTypeCState, GameMatcher.GearTypeCData, GameMatcher.Rotation
		));
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) {
			var speed = gear.gearTypeCData.value._rotateSpeed * gear.gearTypeCState.value.speedMultiplier();
			var newRotation = gear.rotation.value + speed * clock.deltaTime.value;
			var gearRotationSign = gear.rotation.value.sign();
			if (gearRotationSign != 0 && gearRotationSign != newRotation.sign()) {
				newRotation = 0;
			}
			gear.ReplaceRotation(newRotation);
		}
	}
}