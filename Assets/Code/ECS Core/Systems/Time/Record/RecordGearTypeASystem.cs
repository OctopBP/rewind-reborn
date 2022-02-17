using System;
using System.Collections.Generic;
using Entitas;
using Rewind.ECSCore.Enums;

public class RecordGearTypeASystem : ReactiveSystem<GameEntity> {
	readonly GameContext game;

	public RecordGearTypeASystem(Contexts contexts) : base(contexts.game) {
		game = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.GearTypeAState.AddedOrRemoved());

	protected override bool Filter(GameEntity entity) =>
		entity.isGearTypeA && entity.hasGearTypeAState && entity.hasId;

	protected override void Execute(List<GameEntity> entities) {
		if (!game.clockEntity.clockState.value.isRecord()) return;

		foreach (var entity in entities) {
			createTimePoint(entity.id.value, entity.gearTypeAState.value);
		}
	}

	void createTimePoint(Guid id, GearTypeAState isGearOpening) {
		var point = game.CreateEntity();
		point.AddIdRef(id);
		point.AddGearTypeAState(isGearOpening);
		point.AddTimePoint(game.clockEntity.tick.value);
	}
}