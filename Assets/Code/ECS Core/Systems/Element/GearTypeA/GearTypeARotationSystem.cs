using Entitas;
using Rewind.SharedData;
using Rewind.Extensions;

public class GearTypeARotationSystem : IExecuteSystem
{
	private readonly GameEntity clock;
	private readonly IGroup<GameEntity> gears;

	public GearTypeARotationSystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		gears = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.GearTypeA, GameMatcher.GearTypeAState, GameMatcher.GearTypeAData, GameMatcher.Rotation)
		);
	}

	public void Execute()
	{
		foreach (var gear in gears.GetEntities())
		{
			var speed = gear.gearTypeAData.value._rotateSpeed * gear.gearTypeAState.value.SpeedMultiplier();
			var newRotation = gear.rotation.value + speed * clock.deltaTime.value;
			var limitedRotation = newRotation.Clamp(0, gear.gearTypeAData.value._rotateLimit);
			gear.ReplaceRotation(limitedRotation);
		}
	}
}