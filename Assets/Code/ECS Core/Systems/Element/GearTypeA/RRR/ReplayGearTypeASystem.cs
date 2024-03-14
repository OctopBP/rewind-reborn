using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class ReplayGearTypeASystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> gears;
	private readonly IGroup<GameEntity> timePoints;
	private readonly GameEntity clock;

	public ReplayGearTypeASystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;

		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeA, GameMatcher.GearTypeAState, GameMatcher.Id
		));

		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.GearTypeAState, GameMatcher.IdRef
		));
	}

	public void Execute()
	{
		if (!clock.clockState.value.IsReplay()) return;

		foreach (var gear in gears.GetEntities())
		{
			var maybeTimePoint = timePoints.First(
				p => p.timestamp.value <= clock.time.value && p.idRef.value == gear.id.value
			);

			maybeTimePoint.IfSome(timePoint =>
			{
				if (!timePoint.gearTypeAState.value.IsClosedOrOpened())
				{
					gear.ReplaceGearTypeAState(timePoint.gearTypeAState.value);
				}
				timePoint.Destroy();
			});
		}
	}
}