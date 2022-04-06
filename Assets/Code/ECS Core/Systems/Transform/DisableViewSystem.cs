using Entitas;

public class DisableViewSystem : IExecuteSystem {
	readonly IGroup<GameEntity> views;

	public DisableViewSystem(Contexts contexts) => views = contexts.game.GetGroup(GameMatcher.View);

	public void Execute() {
		foreach (var view in views.GetEntities()) {
			view.view.value.SetActive(!view.isViewDisabled);
		}
	}
}