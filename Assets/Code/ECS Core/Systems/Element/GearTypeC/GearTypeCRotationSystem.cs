using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

public class GearTypeCRotationSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> gears;

	public GearTypeCRotationSystem(Contexts contexts) {
		game = contexts.game;
		gears = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeC, GameMatcher.GearTypeCState, GameMatcher.GearTypeCData, GameMatcher.Rotation
		));
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) {
			var speed = gear.gearTypeCData.value.rotateSpeed * gear.gearTypeCState.value.speedMultiplier();
			var newRotation = gear.rotation.value + speed * game.worldTime.value.deltaTime;
			var gearRotationSign = gear.rotation.value.sign();
			if (gearRotationSign != 0 && gearRotationSign != newRotation.sign()) {
				newRotation = 0;
			}
			gear.ReplaceRotation(newRotation);
		}
	}
}