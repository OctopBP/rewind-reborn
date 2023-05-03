using Entitas;
using Rewind.Services;

public class GearTypeBRotationSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> allElements;
	readonly IGroup<GameEntity> gears;

	public GearTypeBRotationSystem(Contexts contexts) {
		game = contexts.game;
		allElements = game.GetGroup(GameMatcher.AllOf(GameMatcher.Id, GameMatcher.Rotation));
		gears = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeB, GameMatcher.IdRef, GameMatcher.GearTypeBData, GameMatcher.Rotation
		));
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) {
			allElements.first(e => e.id.value == gear.idRef.value).IfSome(e => gear.ReplaceRotation(
				e.rotation.value * gear.gearTypeBData.value._multiplier + gear.gearTypeBData.value._offset
			));
		}
	}
}