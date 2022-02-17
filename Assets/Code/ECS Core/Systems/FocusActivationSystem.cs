using Entitas;

public class FocusActivationSystem : IExecuteSystem {
	readonly InputContext input;
	readonly IGroup<GameEntity> focusables;

	public FocusActivationSystem(Contexts contexts) {
		input = contexts.input;
		focusables = contexts.game.GetGroup(GameMatcher.Focusable);
	}

	public void Execute() {
		foreach (var focusable in focusables.GetEntities()) {
			focusable.isActive = input.input.value.getInteractButton() && focusable.isFocus;
		}
	}
}