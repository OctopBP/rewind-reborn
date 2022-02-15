using System.Collections.Generic;
using Entitas;

public class ReplacePreviousPointIndexSystem : ReactiveSystem<GameEntity> {
	public ReplacePreviousPointIndexSystem(Contexts contexts) : base(contexts.game) { }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.MoveComplete);

	protected override bool Filter(GameEntity entity) => entity.hasPreviousPointIndex && entity.hasPointIndex;

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			entity.ReplacePreviousPointIndex(entity.pointIndex.value);
		}
	}
}