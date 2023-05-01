using Code.Helpers.Tracker;

namespace Rewind.Infrastructure {
	public interface IInitWithTracker {
		public void initialize(ITracker tracker);
	}
}