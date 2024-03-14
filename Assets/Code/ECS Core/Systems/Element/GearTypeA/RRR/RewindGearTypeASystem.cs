using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class RewindGearTypeASystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> gears;
	private readonly IGroup<GameEntity> timePoints;
	private readonly GameEntity clock;

	public RewindGearTypeASystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeA, GameMatcher.GearTypeAState, GameMatcher.Id
		));
		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.GearTypeAState,
			GameMatcher.GearTypeAPreviousState, GameMatcher.IdRef
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
				if (!timePoint.gearTypeAPreviousState.value.IsClosedOrOpened())
				{
					gear.ReplaceGearTypeAState(timePoint.gearTypeAPreviousState.value.RewindState());
				}
				timePoint.SetTimePointUsed(true);
			});
		}
	}
}