namespace Rewind.ECSCore.Features {
	public class GameSystems : Feature {
		public GameSystems(Contexts contexts) : base(nameof(GameSystems)) {
			// Init
			Add(new PlacePlayerSystem(contexts));
			
			// Input
			Add(new CommandMoveSystem(contexts));
			Add(new CommandTimeSystem(contexts));
			
			// Move
			Add(new MoveSystem(contexts));
			Add(new PathMoveSystem(contexts));
			Add(new ReplacePreviousPointIndexSystem(contexts));

			Add(new CloneActivateSystem(contexts));
			Add(new GearTypeAStateSystem(contexts));
			Add(new GearTypeARotationSystem(contexts));

			Add(new ReleaseHoldedElementsOnRecordSystem(contexts));
			Add(new ReleaseHoldedElementsByTimeSystem(contexts));

			// Effects
			Add(new FocusSystem(contexts));
			Add(new FocusActivationSystem(contexts));

			// Events
			Add(new GameEventSystems(contexts));
		}
	}
}