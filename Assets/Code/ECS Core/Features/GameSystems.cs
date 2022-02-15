namespace Rewind.ECSCore.Features {
	public class GameSystems : Feature {
		public GameSystems(Contexts contexts) : base(nameof(GameSystems)) {
			Add(new CommandMoveSystem(contexts));
			Add(new MoveSystem(contexts));
			Add(new PathMoveSystem(contexts));
			Add(new FocusSystem(contexts));
			Add(new FocusActivationSystem(contexts));
			Add(new ReplacePreviousPointIndexSystem(contexts));
		}
	}
}