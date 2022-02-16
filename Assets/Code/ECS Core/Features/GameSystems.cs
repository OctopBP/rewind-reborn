namespace Rewind.ECSCore.Features {
	public class GameSystems : Feature {
		public GameSystems(Contexts contexts) : base(nameof(GameSystems)) {
			// Input
			Add(new CommandMoveSystem(contexts));
			Add(new CommandTimeSystem(contexts));
			
			// Move
			Add(new MoveSystem(contexts));
			Add(new PathMoveSystem(contexts));
			Add(new ReplacePreviousPointIndexSystem(contexts));
			
			// Effects
			Add(new FocusSystem(contexts));
			Add(new FocusActivationSystem(contexts));
			
			// Events
			Add(new GameEventSystems(contexts));
		}
	}
}