using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

public class ReplayLeverASystem : IExecuteSystem {
	readonly IGroup<GameEntity> levers;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public ReplayLeverASystem(Contexts contexts) {
		clock = contexts.game.clockEntity;

		levers = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.LeverA, GameMatcher.LeverAState, GameMatcher.Id
		));

		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.LeverAState, GameMatcher.IdRef
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isReplay()) return;

		foreach (var lever in levers.GetEntities()) {
			var maybeTimePoint = timePoints.first(
				p => p.timestamp.value <= clock.time.value && p.idRef.value == lever.id.value
			);

			maybeTimePoint.IfSome(timePoint => {
				lever.ReplaceLeverAState(timePoint.leverAState.value);
				timePoint.Destroy();
			});
		}
	}
}