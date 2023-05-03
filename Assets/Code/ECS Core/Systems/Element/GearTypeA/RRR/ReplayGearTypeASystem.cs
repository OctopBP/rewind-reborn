using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class ReplayGearTypeASystem : IExecuteSystem {
	readonly IGroup<GameEntity> gears;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public ReplayGearTypeASystem(Contexts contexts) {
		clock = contexts.game.clockEntity;

		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeA, GameMatcher.GearTypeAState, GameMatcher.Id
		));

		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.GearTypeAState, GameMatcher.IdRef
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isReplay()) return;

		foreach (var gear in gears.GetEntities()) {
			var maybeTimePoint = timePoints.first(
				p => p.timestamp.value <= clock.time.value && p.idRef.value == gear.id.value
			);

			maybeTimePoint.IfSome(timePoint => {
				if (!timePoint.gearTypeAState.value.isClosedOrOpened()) {
					gear.ReplaceGearTypeAState(timePoint.gearTypeAState.value);
				}
				timePoint.Destroy();
			});
		}
	}
}