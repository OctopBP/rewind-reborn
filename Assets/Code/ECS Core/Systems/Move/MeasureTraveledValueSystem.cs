using Entitas;
using Rewind.Extensions;
using Rewind.Services;

public class MeasureTraveledValueSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> points;
	private readonly IGroup<GameEntity> pathFollowers;

	public MeasureTraveledValueSystem(Contexts contexts)
	{
		points = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.Position)
		);
		pathFollowers = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.PathFollower, GameMatcher.CurrentPoint, GameMatcher.TraveledValue, GameMatcher.Position)
		);
	}

	public void Execute()
	{
		pathFollowers.GetEntities().ForEach(pathFollower =>
        {
			var maybePreviousPoint = pathFollower
				.Filter(e => e.hasPreviousPoint)
				.FlatMap(pf => points.First(p => p.IsSamePoint(pf.previousPoint.value)));
			var maybeCurrentPoint = points.First(pathFollower.IsSamePoint);
			
			maybeCurrentPoint.IfSome(currentPoint =>
            {
				var traveledValue = maybePreviousPoint.FlatMap(previous =>
				{
					var traveled = pathFollower.position.value - previous.position.value;
					var full = currentPoint.position.value - previous.position.value;
					return traveled.magnitude.Divide(full.magnitude);
				}).IfNone(() => 0);

				pathFollower.ReplaceTraveledValue(traveledValue);
			});
		});
	}
}