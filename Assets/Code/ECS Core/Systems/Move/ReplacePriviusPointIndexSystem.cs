using System.Collections.Generic;
using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;

public class ReplacePreviousPointSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext game;

	public ReplacePreviousPointSystem(Contexts contexts) : base(contexts.game)
    {
		game = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.TargetReached);

	protected override bool Filter(GameEntity entity) => entity.hasPreviousPoint && entity.hasCurrentPoint;

	protected override void Execute(List<GameEntity> entities)
    {
		entities.ForEach(ReplacePoints);

		void ReplacePoints(GameEntity entity)
        {
			var currentPoint = entity.currentPoint.value;
			var rewindPoint = entity.previousPoint.value;

			if (game.clockEntity.clockState.value.IsRecord())
            {
				game.CreateMoveTimePoint(
					currentPoint: currentPoint, previousPoint: currentPoint,
					rewindPoint: rewindPoint
				);
			}

			entity.RemovePreviousPoint();
		}
	}
}