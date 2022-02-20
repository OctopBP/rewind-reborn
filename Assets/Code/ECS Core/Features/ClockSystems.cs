namespace Rewind.ECSCore.Features {
	public class ClockSystems : Feature {
		public ClockSystems(Contexts contexts) : base(nameof(ClockSystems)) {
			Add(new TimeSystem(contexts));
			Add(new TimerSystem(contexts));
			Add(new TimeStateSystem(contexts));

			// Record
			Add(new RecordMoveSystem(contexts));
			// Add(new RecordGearTypeASystem(contexts));

			// Rewind
			Add(new RewindMoveSystem(contexts));
			Add(new RewindGearTypeASystem(contexts));
			
			// Replay
			Add(new ReplayMoveSystem(contexts));
			Add(new ReplayGearTypeASystem(contexts));
		}
	}
}