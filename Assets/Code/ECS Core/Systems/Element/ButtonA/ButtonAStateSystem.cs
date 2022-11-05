using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.ECSCore.Helpers;
using static Rewind.ECSCore.Enums.ButtonAState;
using static LanguageExt.Prelude;

public class ButtonAStateSystem : IExecuteSystem {
	readonly GameContext game;
	readonly IGroup<GameEntity> buttons;

	public ButtonAStateSystem(Contexts contexts) {
		game = contexts.game;
		buttons = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.ButtonA, GameMatcher.ButtonAState
		));
	}

	public void Execute() {
		var clockState = game.clockEntity.clockState.value;
		if (clockState.isRewind()) return;

		foreach (var button in buttons.GetEntities()) {
			if (clockState.isReplay() && button.hasHoldedAtTime) continue;

			var currentState = button.buttonAState.value;
			(currentState switch {
				Closed => button.isActive ? Some(Opened) : None,
				Opened => button.isActive ? None : Some(Closed),
				_ => None
			}).IfSome(newState => {
				game.createButtonATimePoint(button.id.value, newState);
				button.ReplaceButtonAState(newState);
				if (clockState.isRecord()) {
					button.ReplaceHoldedAtTime(game.clockEntity.time.value);
				}
			});
		}
	}
}