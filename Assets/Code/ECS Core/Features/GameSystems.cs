namespace Rewind.ECSCore.Features {
	public class GameSystems : Feature {
		public GameSystems(Contexts contexts) : base(nameof(GameSystems)) {
			// Init
			Add(new PlacePlayerSystem(contexts));
			Add(new PointFollowSetupSystem(contexts));

			// Input
			Add(new CommandMoveSystem(contexts));
			Add(new CommandTimeSystem(contexts));

			// Record
			Add(new RecordMoveSystem(contexts));
			Add(new RecordGearTypeASystem(contexts));
			Add(new RecordButtonASystem(contexts));

			// Rewind
			Add(new RewindMoveSystem(contexts));
			Add(new RewindGearTypeASystem(contexts));
			Add(new RewindButtonASystem(contexts));

			// Replay
			Add(new ReplayMoveSystem(contexts));
			Add(new ReplayGearTypeASystem(contexts));
			Add(new ReplayButtonASystem(contexts));

			// Move
			Add(new MoveSystem(contexts));
			Add(new PathMoveSystem(contexts));
			Add(new ReplacePreviousPointIndexSystem(contexts));

			Add(new CloneActivateSystem(contexts));

			// Effects
			Add(new FocusSystem(contexts));
			Add(new FocusActivationSystem(contexts));

			// Elements
			Add(new GearTypeAStateSystem(contexts));
			Add(new GearTypeARotationSystem(contexts));

			Add(new ButtonAStateSystem(contexts));

			Add(new ReleaseHoldedElementsOnRecordSystem(contexts));
			Add(new ReleaseHoldedElementsByTimeSystem(contexts));

			// Logic
			Add(new PuzzleCompletedWhenGearsIsOpenSystem(contexts));
			Add(new PuzzleCompletedWhenButtonsAIsOpenSystem(contexts));
			Add(new ActivatePendulumWhenPuzzleCompletedSystem(contexts));

			// Pendulum
			Add(new PendulumSwaySystem(contexts));
			Add(new PendulumOpenPointSystem(contexts));

			Add(new FollowTransformSystem(contexts));

			// Events
			Add(new GameEventSystems(contexts));
		}
	}
}