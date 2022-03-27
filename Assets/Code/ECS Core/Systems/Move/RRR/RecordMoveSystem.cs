using System.Collections.Generic;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.ECSCore.Helpers;

public class RecordMoveSystem : ReactiveSystem<GameEntity> {
	readonly GameContext game;

	public RecordMoveSystem(Contexts contexts) : base(contexts.game) {
		game = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.AnyOf(
			GameMatcher.PointIndex, GameMatcher.PreviousPointIndex, GameMatcher.PathIndex,
			GameMatcher.PreviousPathIndex
		));
	}

	protected override bool Filter(GameEntity entity) =>
		entity.isPlayer && entity.hasPointIndex && entity.hasPreviousPointIndex &&
		entity.hasPathIndex && entity.hasPreviousPathIndex && entity.hasRewindPointIndex;

	protected override void Execute(List<GameEntity> entities) {
		if (!game.clockEntity.clockState.value.isRecord()) return;

		foreach (var entity in entities) {
			game.createMoveTimePoint(
				entity.pointIndex.value, entity.previousPointIndex.value, entity.pathIndex.value,
				entity.previousPathIndex.value, entity.rewindPointIndex.value
			);
		}
	}
}