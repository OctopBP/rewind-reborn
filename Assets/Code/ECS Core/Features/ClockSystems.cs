namespace Rewind.ECSCore.Features {
	public class ClockSystems : Feature {
		public ClockSystems(Contexts contexts) : base(nameof(ClockSystems)) {
			Add(new TimeSystem(contexts));
		}
	}
}