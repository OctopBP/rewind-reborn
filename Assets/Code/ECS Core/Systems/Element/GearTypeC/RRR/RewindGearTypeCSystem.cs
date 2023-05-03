using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class RewindGearTypeCSystem : IExecuteSystem {
	readonly IGroup<GameEntity> gears;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public RewindGearTypeCSystem(Contexts contexts) {
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

	public void Execute() {
		if (!clock.clockState.value.isRewind()) return;

		foreach (var gear in gears.GetEntities()) {
			var maybeTimePoint = timePoints.first(
				p => p.timestamp.value >= clock.time.value && p.idRef.value == gear.id.value
			);

			maybeTimePoint.IfSome(timePoint => {
				// if (!timePoint.gearTypeCPreviousState.value.isClosed()) {
				gear.ReplaceGearTypeCState(timePoint.gearTypeCPreviousState.value.rewindState());
				// }
				timePoint.SetTimePointUsed(true);
			});
		}
	}
}