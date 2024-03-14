using System.Collections.Generic;
using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;

public class RecordGearTypeCSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext game;
	private readonly IGroup<GameEntity> gears;

	public RecordGearTypeCSystem(Contexts contexts) : base(contexts.game)
	{
		game = contexts.game;
		gears = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeC, GameMatcher.GearTypeCData, GameMatcher.Rotation
		));
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.AnyOf(GameMatcher.ClockState).AddedOrRemoved());

	protected override bool Filter(GameEntity entity) => true;

	protected override void Execute(List<GameEntity> _)
	{
		// Trigger when it becomes Rewind 
		if (!game.clockEntity.clockState.value.IsRewind()) return;

		foreach (var gear in gears.GetEntities())
		{
			game.CreateGearCTimePoint(
				gear.id.value, gear.gearTypeCState.value, gear.gearTypeCState.value, gear.rotation.value
			);
		}
	}
}