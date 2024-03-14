using Entitas;
using Rewind.Services;

public class FocusSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> focusables;
	private readonly IGroup<GameEntity> players;

	public FocusSystem(Contexts contexts)
	{
		focusables = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Focusable, GameMatcher.CurrentPoint
		));
		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.CurrentPoint
		));
	}

	public void Execute()
	{
		foreach (var focusable in focusables.GetEntities())
		{
			var onPoint = players.Any(p => PlayerOnPoint(focusable, p) && p.IsMoveComplete());
			focusable.SetFocus(onPoint);
		}

		bool PlayerOnPoint(GameEntity point, GameEntity player) =>
			point.currentPoint.value == player.currentPoint.value &&
			!player.hasPreviousPoint;
	}
}