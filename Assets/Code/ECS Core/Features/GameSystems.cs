namespace Rewind.ECSCore.Features {
	public class GameSystems : Feature {
		public GameSystems(Contexts contexts) : base(nameof(GameSystems)) {
			// Init
			Add(new PlaceCharactersSystem(contexts));
			Add(new PointFollowSetupSystem(contexts));

			// Input
			Add(new CommandMoveSystem(contexts));
			Add(new CommandConnectorsMoveSystem(contexts));
			Add(new CommandTimeSystem(contexts));

			// Record
			Add(new RecordMoveSystem(contexts));
			Add(new RecordGearTypeASystem(contexts));
			Add(new RecordGearTypeCSystem(contexts));
			Add(new RecordButtonASystem(contexts));
			Add(new RecordLeverASystem(contexts));

			// Rewind
			Add(new RewindMoveSystem(contexts));
			Add(new RewindGearTypeASystem(contexts));
			Add(new RewindGearTypeCSystem(contexts));
			Add(new RewindButtonASystem(contexts));
			Add(new RewindLeverASystem(contexts));

			// Replay
			Add(new ReplayMoveSystem(contexts));
			Add(new ReplayGearTypeASystem(contexts));
			Add(new ReplayGearTypeCSystem(contexts));
			Add(new ReplayButtonASystem(contexts));
			Add(new ReplayLeverASystem(contexts));

			// Move
			Add(new MoveSystem(contexts));
			Add(new PathMoveSystem(contexts));
			Add(new ReplacePreviousPointIndexSystem(contexts));
			Add(new MoveStateSystem(contexts));

			Add(new ApplyDepthSystem(contexts));

			Add(new CloneActivateSystem(contexts));

			// Effects
			Add(new FocusSystem(contexts));
			Add(new FocusActivationSystem(contexts));
			Add(new FocusActivationSecondSystem(contexts));

			// Elements
			Add(new GearTypeAStateSystem(contexts));
			Add(new GearTypeARotationSystem(contexts));
			Add(new GearTypeBRotationSystem(contexts));
			Add(new GearTypeCStateSystem(contexts));
			Add(new GearTypeCRotationSystem(contexts));
			Add(new ButtonAStateSystem(contexts));
			Add(new LeverAStateSystem(contexts));

			Add(new ReleaseHoldedElementsOnRecordSystem(contexts));
			Add(new ReleaseHoldedElementsByTimeSystem(contexts));

			Add(new ConnectorOpenSystem(contexts));	

			// Logic
			Add(new PuzzleCompletedWhenElementsAIsDoneSystem(contexts));
			Add(new CheckPuzzleGearTypeCIsDoneSystem(contexts));
			Add(new CheckPuzzleElementsIsDoneSystem(contexts));	
			
			Add(new ActivatePendulumWhenPuzzleCompletedSystem(contexts));
			Add(new ActivatePlatformAWhenPuzzleCompletedSystem(contexts));
			Add(new ActivateDoorAWhenPuzzleCompletedSystem(contexts));

			Add(new CheckFinishSystem(contexts));	

			// Pendulum
			Add(new PendulumSwaySystem(contexts));
			Add(new PendulumOpenPointSystem(contexts));

			// Platform A
			Add(new PlatformAMoveSystem(contexts));
			
			// Door A
			Add(new DoorAOpenPointSystem(contexts));

			Add(new FollowTransformSystem(contexts));

			// Events
			Add(new GameEventSystems(contexts));
		}
	}
}