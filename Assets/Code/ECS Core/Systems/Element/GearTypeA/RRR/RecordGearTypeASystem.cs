using System.Collections.Generic;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.ECSCore.Helpers;

public class RecordGearTypeASystem : ReactiveSystem<GameEntity> {
	readonly GameContext game;
	readonly IGroup<GameEntity> gears;

	public RecordGearTypeASystem(Contexts contexts) : base(contexts.game) {
		game = contexts.game;
		gears = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeA, GameMatcher.GearTypeAData, GameMatcher.Rotation
		));
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.AnyOf(GameMatcher.ClockState).AddedOrRemoved());

	protected override bool Filter(GameEntity entity) => true;

	protected override void Execute(List<GameEntity> _) {
		// Trigger when it becomes Rewind 
		if (!game.clockEntity.clockState.value.isRewind()) return;

		foreach (var gear in gears.GetEntities()) {
			game.createGearATimePoint(
				gear.id.value, gear.gearTypeAState.value, gear.gearTypeAState.value, gear.rotation.value
			);
		}
	}
}