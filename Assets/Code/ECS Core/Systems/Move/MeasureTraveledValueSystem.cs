using Entitas;
using Rewind.Extensions;
using Rewind.Services;

public class MeasureTraveledValueSystem : IExecuteSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> pathFollowers;

	public MeasureTraveledValueSystem(Contexts contexts) {
		points = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.Position)
		);
		pathFollowers = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.PathFollower, GameMatcher.CurrentPoint, GameMatcher.TraveledValue, GameMatcher.Position)
		);
	}

	public void Execute() {
		pathFollowers.GetEntities().forEach(pathFollower => {
			var maybePreviousPoint = pathFollower
				.filter(e => e.hasPreviousPoint)
				.flatMap(pf => points.first(p => p.isSamePoint(pf.previousPoint.value)));
			var maybeCurrentPoint = points.first(pathFollower.isSamePoint);
			
			maybeCurrentPoint.IfSome(currentPoint => {
				var traveledValue = maybePreviousPoint.flatMap(previous => {
					var traveled = pathFollower.position.value - previous.position.value;
					var full = currentPoint.position.value - previous.position.value;
					return traveled.magnitude.divide(full.magnitude);
				}).IfNone(() => 0);

				pathFollower.ReplaceTraveledValue(traveledValue);
			});
		});
	}
}