using Entitas;
using Rewind.ECSCore.Enums;
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
			GameMatcher.TimePoint, GameMatcher.GearTypeAState, GameMatcher.IdRef
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isReplay()) return;

		foreach (var gear in gears.GetEntities()) {
			timePoints.first(p => p.timePoint.value >= clock.time.value && p.idRef.value == gear.id.value)
				.IfSome(timePoint => gear.ReplaceGearTypeAState(timePoint.gearTypeAState.value));
		}
	}
}