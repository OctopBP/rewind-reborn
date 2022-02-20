using System;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;

public class GearTypeAStateSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> gears;

	public GearTypeAStateSystem(Contexts contexts) {
		game = contexts.game;
		gears = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeA, GameMatcher.GearTypeAData, GameMatcher.Rotation
		));
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) {
			var newState = gear.isActive ? GearTypeAState.Opening : GearTypeAState.Closing;

			// if (newRotation >= gear.gearTypeAData.value.rotateLimit) {
			// 	newState = GearTypeAState.Opened;
			// } else if (newRotation <= 0) {
			// 	newState = GearTypeAState.Closed;
			// }

			var currentState = gear.gearTypeAState.value;
			if (newState != currentState && game.clockEntity.clockState.value.isRecord()) {
				currentState = newState;
				createTimePoint(gear.id.value, newState);
				gear.ReplaceGearTypeAState(newState);
			}

			var speed = gear.gearTypeAData.value.rotateSpeed * currentState.speedMultiplier();
			var newRotation = gear.rotation.value + speed * game.time.value.deltaTime;
			var limitedRotation = newRotation.clamp(0, gear.gearTypeAData.value.rotateLimit);
			gear.ReplaceRotation(limitedRotation);
		}
	}

	void createTimePoint(Guid id, GearTypeAState state) {
		var point = game.CreateEntity();
		point.AddIdRef(id);
		point.AddGearTypeAState(state);
		point.AddTimePoint(game.clockEntity.tick.value);
	}
}