using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

public class ReplayButtonASystem : IExecuteSystem {
	readonly IGroup<GameEntity> buttons;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public ReplayButtonASystem(Contexts contexts) {
		clock = contexts.game.clockEntity;

		buttons = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.ButtonA, GameMatcher.ButtonAState, GameMatcher.Id
		));

		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.TimePoint, GameMatcher.ButtonAState, GameMatcher.IdRef
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isReplay()) return;

		foreach (var button in buttons.GetEntities()) {
			var maybeTimePoint = timePoints.first(
				p => p.timePoint.value <= clock.time.value && p.idRef.value == button.id.value
			);

			{if (maybeTimePoint.valueOut(out var timePoint)) {
				button.ReplaceButtonAState(timePoint.buttonAState.value);
				timePoint.Destroy();
			}}
		}
	}
}