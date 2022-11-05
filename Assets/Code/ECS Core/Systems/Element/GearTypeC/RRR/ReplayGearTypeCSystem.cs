using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

public class ReplayGearTypeCSystem : IExecuteSystem {
	readonly IGroup<GameEntity> gears;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public ReplayGearTypeCSystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.GearTypeC, GameMatcher.GearTypeCState, GameMatcher.Id
		));
		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.GearTypeCState, GameMatcher.IdRef
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isReplay()) return;

		foreach (var gear in gears.GetEntities()) {
			var maybeTimePoint = timePoints.first(
				p => p.timestamp.value <= clock.time.value && p.idRef.value == gear.id.value
			);

			{if (maybeTimePoint.valueOut(out var timePoint)) {
				// if (!timePoint.gearTypeCState.value.isClosed()) {
					gear.ReplaceGearTypeCState(timePoint.gearTypeCState.value);
				// } 
				timePoint.Destroy();
			}}
		}
	}
}