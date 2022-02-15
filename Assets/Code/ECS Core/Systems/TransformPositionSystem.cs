using System.Collections.Generic;
using Entitas;

public class TransformPositionSystem : ReactiveSystem<GameEntity> {
	public TransformPositionSystem(Contexts contexts) : base(contexts.game) { }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.Position);
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasPosition && entity.hasView;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			entity.view.Value.transform.position = entity.position.value;
		}
	}
}