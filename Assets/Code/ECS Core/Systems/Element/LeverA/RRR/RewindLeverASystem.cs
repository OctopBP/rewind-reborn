using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class RewindLeverASystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> levers;
	private readonly IGroup<GameEntity> timePoints;
	private readonly GameEntity clock;

	public RewindLeverASystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		levers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.LeverA, GameMatcher.LeverAState, GameMatcher.Id
		));
		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.LeverAState, GameMatcher.IdRef
		).NoneOf(
			GameMatcher.TimePointUsed
		));
	}

	public void Execute()
	{
		if (!clock.clockState.value.IsRewind()) return;

		foreach (var lever in levers.GetEntities())
		{
			var maybeTimePoint = timePoints.First(
				p => p.timestamp.value >= clock.time.value && p.idRef.value == lever.id.value
			);

			maybeTimePoint.IfSome(timePoint =>
			{
				lever.ReplaceLeverAState(timePoint.leverAState.value.RewindState());
				timePoint.SetTimePointUsed(true);
			});
		}
	}
}