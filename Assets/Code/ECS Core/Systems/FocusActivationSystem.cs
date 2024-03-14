using Entitas;

public class FocusActivationSystem : IExecuteSystem
{
	private readonly InputContext input;
	private readonly IGroup<GameEntity> focusables;

	public FocusActivationSystem(Contexts contexts)
	{
		input = contexts.input;
		focusables = contexts.game.GetGroup(GameMatcher.Focusable);
	}

	public void Execute()
	{
		foreach (var focusable in focusables.GetEntities())
		{
			focusable.SetActive(input.input.value.GetInteractButton() && focusable.isFocus);
		}
	}
}