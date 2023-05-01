using System;

namespace Code.Helpers.Tracker {
	public interface ITracker {
		public void track(Action action);
	}
}