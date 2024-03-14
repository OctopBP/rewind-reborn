namespace Rewind.ECSCore.Features
{
	public class ClockSystems : Feature
	{
		public ClockSystems(Contexts contexts) : base(nameof(ClockSystems))
		{
			Add(new DeltaTimeSystem(contexts));
			Add(new TimeSystem(contexts));
			Add(new TimerSystem(contexts));
			Add(new TimeStateSystem(contexts));
		}
	}
}