using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class RewindGearTypeASystem : IExecuteSystem {
	readonly IGroup<GameEntity> gears;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public RewindGearTypeASystem(Contexts contexts) {
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

	public void Execute() {
		if (!clock.clockState.value.isRewind()) return;

		foreach (var gear in gears.GetEntities()) {
			var maybeTimePoint = timePoints.first(
				p => p.timestamp.value >= clock.time.value && p.idRef.value == gear.id.value
			);

			maybeTimePoint.IfSome(timePoint => {
				if (!timePoint.gearTypeAPreviousState.value.isClosedOrOpened()) {
					gear.ReplaceGearTypeAState(timePoint.gearTypeAPreviousState.value.rewindState());
				}
				timePoint.isTimePointUsed = true;
			});
		}
	}
}