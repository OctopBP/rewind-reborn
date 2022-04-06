using System.Collections.Generic;
using Entitas;

public class ApplyDepthToPositionZSystem : ReactiveSystem<GameEntity> {
	public ApplyDepthToPositionZSystem(Contexts contexts) : base(contexts.game) { }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.AllOf(GameMatcher.Depth));

	protected override bool Filter(GameEntity entity) => entity.hasDepth && entity.hasView;

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			var position = entity.view.value.transform.position;
			position.z = -entity.depth.value;
			entity.view.value.transform.position = position;
		}
	}
}