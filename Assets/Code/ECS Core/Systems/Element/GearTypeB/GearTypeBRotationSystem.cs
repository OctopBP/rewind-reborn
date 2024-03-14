using Entitas;
using Rewind.Services;

public class GearTypeBRotationSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> allElements;
	private readonly IGroup<GameEntity> gears;

	public GearTypeBRotationSystem(Contexts contexts)
	{
		allElements = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Id, GameMatcher.Rotation));
		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeB, GameMatcher.IdRef, GameMatcher.GearTypeBData, GameMatcher.Rotation
		));
	}

	public void Execute()
	{
		foreach (var gear in gears.GetEntities()) {
			allElements.First(e => e.id.value == gear.idRef.value).IfSome(e => gear.ReplaceRotation(
				e.rotation.value * gear.gearTypeBData.value._multiplier + gear.gearTypeBData.value._offset
			));
		}
	}
}