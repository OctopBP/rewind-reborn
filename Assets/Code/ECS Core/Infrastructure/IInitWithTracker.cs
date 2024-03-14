using Code.Helpers.Tracker;

namespace Rewind.Infrastructure
{
	public interface IInitWithTracker
	{
		public void Initialize(ITracker tracker);
	}
}