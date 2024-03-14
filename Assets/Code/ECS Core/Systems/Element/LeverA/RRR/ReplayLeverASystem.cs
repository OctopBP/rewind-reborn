using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class ReplayLeverASystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> levers;
	private readonly IGroup<GameEntity> timePoints;
	private readonly GameEntity clock;

	public ReplayLeverASystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;

		levers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.LeverA, GameMatcher.LeverAState, GameMatcher.Id
		));

		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.LeverAState, GameMatcher.IdRef
		));
	}

	public void Execute()
	{
		if (!clock.clockState.value.IsReplay()) return;

		foreach (var lever in levers.GetEntities())
		{
			var maybeTimePoint = timePoints.First(
				p => p.timestamp.value <= clock.time.value && p.idRef.value == lever.id.value
			);

			maybeTimePoint.IfSome(timePoint =>
			{
				lever.ReplaceLeverAState(timePoint.leverAState.value);
				timePoint.Destroy();
			});
		}
	}
}