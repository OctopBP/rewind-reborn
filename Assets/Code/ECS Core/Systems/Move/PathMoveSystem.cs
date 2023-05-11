using Entitas;
using Rewind.Extensions;
using Rewind.Services;

public class PathMoveSystem : IExecuteSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> pathFollowers;
	readonly GameEntity clock;

	public PathMoveSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		points = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.Position)
		);
		pathFollowers = contexts.game.GetGroup(GameMatcher
			.AllOf(
				GameMatcher.PathFollower, GameMatcher.Movable, GameMatcher.CurrentPoint,
				GameMatcher.TraveledValue, GameMatcher.PathFollowerSpeed, GameMatcher.Position
			)
		);
	}

	public void Execute() {
		pathFollowers.GetEntities().forEach(pathFollower => {
			var maybePreviousPoint = pathFollower
				.filter(e => e.hasPreviousPoint)
				.flatMap(pf => points.first(p => p.isSamePoint(pf.previousPoint.value)));
			var maybeCurrentPoint = points.first(pathFollower.isSamePoint);
			
			maybeCurrentPoint.IfSome(currentPoint => {
				var targetPos = currentPoint.position.value;
				var newBasePosition = maybePreviousPoint.Match(
					previous => {
						var fullPath = targetPos - previous.position.value;
						return previous.position.value + fullPath * pathFollower.traveledValue.clampedValue();
					},
					() => targetPos
				);
				
				var direction = targetPos - newBasePosition;
				var deltaMove = direction.normalized * pathFollower.pathFollowerSpeed.value * clock.deltaTime.value;

				var targetReached = deltaMove.magnitude >= direction.magnitude;
				var newPosition = targetReached ? targetPos : newBasePosition + deltaMove;

				pathFollower
					.ReplacePosition(newPosition)
					.SetTargetReached(targetReached);
			});
		});
	}
}