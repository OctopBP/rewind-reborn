using Entitas;
using Rewind.ECSCore.Enums;

public class GearTypeAStateSystem : IExecuteSystem {
	readonly IGroup<GameEntity> gears;

	public GearTypeAStateSystem(Contexts contexts) {
		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeA, GameMatcher.GearTypeAData, GameMatcher.Rotation
		));
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) {
			var newState = gear.rotation.value switch {
				<= 0 => GearTypeAState.Closed,
				var x when x >= gear.gearTypeAData.value.rotateLimit => GearTypeAState.Opened,
				_ => gear.isActive ? GearTypeAState.Opening : GearTypeAState.Closing
			};

			if (newState != gear.gearTypeAState.value)
				gear.ReplaceGearTypeAState(newState);
		}
	}
}