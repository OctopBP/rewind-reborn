using System.Linq;
using Entitas;
using Rewind.SharedData;
using Rewind.Extensions;

public class ReplayMoveSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> clones;
	private readonly IGroup<GameEntity> timePoints;
	private readonly GameEntity clock;

	public ReplayMoveSystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		clones = contexts.game.GetGroup(
			GameMatcher.Clone
		);
		timePoints = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Timestamp, GameMatcher.CurrentPoint, GameMatcher.PreviousPoint)
		);
	}

	public void Execute()
	{
		if (!clock.clockState.value.IsReplay()) return;
		
		foreach (var clone in clones.GetEntities())
        {
			FunctionalExtensions.First(timePoints
					.GetEntities()
					.Where(p => p.timestamp.value < clock.time.value)
					.OrderBy(tp => tp.timestamp.value))
				.IfSome(timePoint => UseTimePoint(clone, timePoint));
		}

		void UseTimePoint(GameEntity clone, GameEntity timePoint)
        {
			clone.ReplaceCurrentPoint(timePoint.currentPoint.value);
			clone.ReplacePreviousPoint(timePoint.previousPoint.value);

			timePoint.Destroy();
		}
	}
}