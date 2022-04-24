using Entitas;

public class FocusActivationSecondSystem : IExecuteSystem {
	readonly InputContext input;
	readonly IGroup<GameEntity> focusables;

	public FocusActivationSecondSystem(Contexts contexts) {
		input = contexts.input;
		focusables = contexts.game.GetGroup(GameMatcher.Focusable);
	}

	public void Execute() {
		foreach (var focusable in focusables.GetEntities()) {
			focusable.isActiveSecond = input.input.value.getInteractSecondButton() && focusable.isFocus;
		}
	}
}