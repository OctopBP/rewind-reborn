using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class RewindGearTypeCSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> gears;
	private readonly IGroup<GameEntity> timePoints;
	private readonly GameEntity clock;

	public RewindGearTypeCSystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeC, GameMatcher.GearTypeCState, GameMatcher.Id
		));
		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.GearTypeCState,
			GameMatcher.GearTypeCPreviousState, GameMatcher.IdRef
		).NoneOf(
			GameMatcher.TimePointUsed
		));
	}

	public void Execute()
	{
		if (!clock.clockState.value.IsRewind()) return;

		foreach (var gear in gears.GetEntities())
		{
			var maybeTimePoint = timePoints.First(
				p => p.timestamp.value >= clock.time.value && p.idRef.value == gear.id.value
			);

			maybeTimePoint.IfSome(timePoint =>
			{
				// if (!timePoint.gearTypeCPreviousState.value.isClosed()) {
				gear.ReplaceGearTypeCState(timePoint.gearTypeCPreviousState.value.RewindState());
				// }
				timePoint.SetTimePointUsed(true);
			});
		}
	}
}