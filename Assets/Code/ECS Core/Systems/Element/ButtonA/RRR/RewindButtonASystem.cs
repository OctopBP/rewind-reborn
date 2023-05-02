using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class RewindButtonASystem : IExecuteSystem {
	readonly IGroup<GameEntity> buttons;
	readonly IGroup<GameEntity> timePoints;
	readonly GameEntity clock;

	public RewindButtonASystem(Contexts contexts) {
		clock = contexts.game.clockEntity;
		buttons = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.ButtonA, GameMatcher.ButtonAState, GameMatcher.Id
		));
		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.ButtonAState, GameMatcher.IdRef
		).NoneOf(
			GameMatcher.TimePointUsed
		));
	}

	public void Execute() {
		if (!clock.clockState.value.isRewind()) return;

		foreach (var button in buttons.GetEntities()) {
			var maybeTimePoint = timePoints.first(
				p => p.timestamp.value >= clock.time.value && p.idRef.value == button.id.value
			);

			maybeTimePoint.IfSome(timePoint => {
				button.ReplaceButtonAState(timePoint.buttonAState.value.rewindState());
				timePoint.isTimePointUsed = true;
			});
		}
	}
}