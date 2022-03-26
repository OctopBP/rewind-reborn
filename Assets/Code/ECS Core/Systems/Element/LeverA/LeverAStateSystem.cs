using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.ECSCore.Helpers;

public class LeverAStateSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> levers;
	readonly InputContext input;

	public LeverAStateSystem(Contexts contexts) {
		game = contexts.game;
		input = contexts.input;
		levers = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.LeverA, GameMatcher.LeverAState
		));
	}

	public void Execute() {
		var clockState = game.clockEntity.clockState.value;
		if (clockState.isRewind()) return;

		foreach (var lever in levers.GetEntities()) {
			if (clockState.isReplay() && lever.hasHoldedAtTime) continue;
			if (!lever.isActive || !input.input.value.getInteractButtonDown()) continue;

			var newState = lever.leverAState.value.rewindState();
			game.createLeverATimePoint(lever.id.value, newState);
			lever.ReplaceLeverAState(newState);
			if (clockState.isRecord()) {
				lever.ReplaceHoldedAtTime(game.clockEntity.time.value);
			}
		}
	}
}