using Entitas;
using Rewind.Services;

public class CheckPointTriggerSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> pointTriggers;
	private readonly IGroup<GameEntity> players;

	public CheckPointTriggerSystem(Contexts contexts)
	{
		pointTriggers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PointTrigger, GameMatcher.CurrentPoint
		));
		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.CurrentPoint
		));
	}

	public void Execute()
	{
		foreach (var pointTrigger in pointTriggers.GetEntities())
		{
			foreach (var player in players.GetEntities())
			{
				if (pointTrigger.IsSamePoint(player))
				{
					pointTrigger
						.SetPointTriggerReached(true)
						.SetPointTrigger(false);
				}
			}
		}
	}
}