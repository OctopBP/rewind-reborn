using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
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
			GameMatcher.TimePoint, GameMatcher.GearTypeCState,
			GameMatcher.GearTypeCPreviousState, GameMatcher.IdRef
		).NoneOf(
			GameMatcher.TimePointUsed
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isRewind()) return;

		foreach (var gear in gears.GetEntities()) {
			var maybeTimePoint = timePoints.first(
				p => p.timePoint.value >= clock.time.value && p.idRef.value == gear.id.value
			);

			{if (maybeTimePoint.valueOut(out var timePoint)) {
				// if (!timePoint.gearTypeCPreviousState.value.isClosed()) {
					gear.ReplaceGearTypeCState(timePoint.gearTypeCPreviousState.value.rewindState());
				// }
				timePoint.isTimePointUsed = true;
			}}
		}
	}
}