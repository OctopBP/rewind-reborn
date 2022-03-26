using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

public class RewindLeverASystem : IExecuteSystem {
	readonly IGroup<GameEntity> levers;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public RewindLeverASystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		levers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.LeverA, GameMatcher.LeverAState, GameMatcher.Id
		));
		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.TimePoint, GameMatcher.LeverAState, GameMatcher.IdRef
		).NoneOf(
			GameMatcher.TimePointUsed
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isRewind()) return;

		foreach (var lever in levers.GetEntities()) {
			var maybeTimePoint = timePoints.first(
				p => p.timePoint.value >= clock.time.value && p.idRef.value == lever.id.value
			);

			{if (maybeTimePoint.valueOut(out var timePoint)) {
				lever.ReplaceLeverAState(timePoint.leverAState.value.rewindState());
				timePoint.isTimePointUsed = true;
			}}
		}
	}
}