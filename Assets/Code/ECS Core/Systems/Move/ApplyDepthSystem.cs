using System.Collections.Generic;
using Entitas;
using Rewind.Services;

public class ApplyDepthSystem : ReactiveSystem<GameEntity> {
	readonly IGroup<GameEntity> points;

	public ApplyDepthSystem(Contexts contexts) : base(contexts.game) {
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.Depth)
		);
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.PreviousPointIndex);

	protected override bool Filter(GameEntity entity) => entity.isCharacter && entity.hasPreviousPointIndex;

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			points.first(entity.isSamePoint).IfSome(p => entity.ReplaceDepth(p.depth.value));
		}
	}
}