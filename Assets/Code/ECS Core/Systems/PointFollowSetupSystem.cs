using Entitas;
using Rewind.Services;

public class PointFollowSetupSystem : IInitializeSystem
{
	private readonly IGroup<GameEntity> points;
	private readonly IGroup<GameEntity> followers;

	public PointFollowSetupSystem(Contexts contexts)
	{
		points = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint));
		followers = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.CurrentPoint, GameMatcher.FollowTransform));
	}

	public void Initialize()
	{
		foreach (var point in points.GetEntities())
		{
			foreach (var follower in followers.GetEntities())
			{
				if (point.IsSamePoint(follower))
				{
					point.AddFollowTransform(follower.followTransform.value);
				}
			}
		}
	}
}