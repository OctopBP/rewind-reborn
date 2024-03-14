using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class CloneActivateSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> clones;
	private readonly IGroup<GameEntity> players;
	private readonly GameEntity clock;

	public CloneActivateSystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		clones = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Clone, GameMatcher.View)
		);
		players = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Player, GameMatcher.CurrentPoint, GameMatcher.Position)
		);
	}

	public void Execute()
	{
		foreach (var clone in clones.GetEntities())
		{
			var viewDisabled = clone.isViewDisabled;
			var needUpdate = viewDisabled == clock.clockState.value.IsReplay();

			if (needUpdate)
			{
				players.First().IfSome(player => clone
					.ReplacePosition(player.position.value)
					.ReplaceCurrentPoint(player.currentPoint.value)
					.SetViewDisabled(!viewDisabled));
			}
		}
	}
}
