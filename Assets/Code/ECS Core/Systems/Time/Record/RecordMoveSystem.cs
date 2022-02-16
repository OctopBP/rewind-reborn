using System.Collections.Generic;
using Entitas;
using Rewind.ECSCore.Enums;

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
		entity.isCharacter && entity.hasPointIndex && entity.hasPreviousPointIndex &&
		entity.hasPathIndex && entity.hasPreviousPathIndex && entity.hasRewindPointIndex;

	protected override void Execute(List<GameEntity> entities) {
		if (!game.clockEntity.clockState.value.isRecord()) return;

		foreach (var entity in entities) {
			createTimePoint(
				entity.pointIndex.value, entity.previousPointIndex.value, entity.pathIndex.value,
				entity.previousPathIndex.value, entity.rewindPointIndex.value
			);
		}
	}

	void createTimePoint(
		int pointIndex, int previousPointIndex, int pathIndex, int previousPathIndex, int rewindPointIndex
	) {
		var point = game.CreateEntity();

		point.AddTimePoint(game.clockEntity.tick.value);
		
		point.AddPointIndex(pointIndex);
		point.AddPreviousPointIndex(previousPointIndex);
		point.AddRewindPointIndex(rewindPointIndex);
		
		point.AddPathIndex(pathIndex);
		point.AddPreviousPathIndex(previousPathIndex);
	}
}