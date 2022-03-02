using System.Collections.Generic;
using Entitas;
using Rewind.ECSCore.Enums;

public class ReleaseHoldedElementsOnRecordSystem : ReactiveSystem<GameEntity> {
	readonly GameEntity clockEntity;
	readonly IGroup<GameEntity> elements;

	public ReleaseHoldedElementsOnRecordSystem(Contexts contexts) : base(contexts.game) {
		clockEntity = contexts.game.clockEntity;
		elements = contexts.game.GetGroup(GameMatcher.HoldedAtTime);
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.AnyOf(GameMatcher.ClockState).AddedOrRemoved());

	protected override bool Filter(GameEntity entity) => true;

	protected override void Execute(List<GameEntity> _) {
		// Trigger when it becomes Record 
		if (!clockEntity.clockState.value.isRecord()) return;

		foreach (var element in elements.GetEntities()) {
			element.RemoveHoldedAtTime();
		}
	}
}