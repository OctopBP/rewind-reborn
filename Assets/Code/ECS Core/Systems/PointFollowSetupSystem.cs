using Entitas;
using Rewind.Services;

public class PointFollowSetupSystem : IInitializeSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> followers;

	public PointFollowSetupSystem(Contexts contexts) {
		points = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint));
		followers = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.CurrentPoint, GameMatcher.FollowTransform));
	}

	public void Initialize() {
		foreach (var point in points.GetEntities()) {
			foreach (var follower in followers.GetEntities()) {
				if (point.isSamePoint(follower)) {
					point.AddFollowTransform(follower.followTransform.value);
				}
			}
		}
	}
}