using System.Collections.Generic;
using Entitas;

public class RecordMoveSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext _game;

	public RecordMoveSystem(Contexts contexts) : base(contexts.game)
	{
		_game = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context
			.CreateCollector(GameMatcher
				.AnyOf(
					GameMatcher.PointIndex,
					GameMatcher.PreviousPointIndex,
					GameMatcher.PathIndex,
					GameMatcher.PreviousPathIndex));
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isPlayer
			&& entity.hasPointIndex
			&& entity.hasPreviousPointIndex
			&& entity.hasPathIndex
			&& entity.hasPreviousPathIndex
			&& entity.hasRewindPointIndex;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		if (_game.clockEntity.isRewind) return;
		if (_game.clockEntity.isReplay) return;

		foreach (GameEntity entity in entities)
		{
			CreateTimePoint(
				entity.pointIndex.Value,
				entity.previousPointIndex.Value,
				entity.pathIndex.Value,
				entity.previousPathIndex.Value,
				entity.rewindPointIndex.Value);
		}
	}

	private void CreateTimePoint(int pointIndex, int previousPointIndex, int pathIndex, int previousPathIndex, int rewindPointIndex)
	{
		_game.logger.Value.LogMessage($"[{_game.clockEntity.timeTick.Value}] RecordMove CreateTimePoint pointIndex:{pointIndex} previousPointIndex:{previousPointIndex}");

		GameEntity point = _game.CreateEntity();

		point.AddTimePoint(_game.clockEntity.timeTick.Value);

		point.AddPointIndex(pointIndex);
		point.AddPreviousPointIndex(previousPointIndex);

		point.AddPathIndex(pathIndex);
		point.AddPreviousPathIndex(previousPathIndex);

		point.AddRewindPointIndex(rewindPointIndex);
	}
}