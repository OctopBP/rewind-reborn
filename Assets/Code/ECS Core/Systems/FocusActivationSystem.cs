using Entitas;

public class FocusActivationSystem : IExecuteSystem {
	readonly GameContext game;
	readonly InputContext input;
	readonly IGroup<GameEntity> focuseds;

	public FocusActivationSystem(Contexts contexts) {
		game = contexts.game;
		input = contexts.input;
		focuseds = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Focusable));
	}

	public void Execute() {
		foreach (var focused in focuseds.GetEntities()) {
			focused.isActive = input.input.value.getInteractButton() && focused.isFocus;
		}
	}
}