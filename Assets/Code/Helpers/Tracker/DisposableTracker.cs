using System;

namespace Code.Helpers.Tracker {
	public class DisposableTracker : IDisposableTracker {
		Action onDispose;

		public void track(Action action) {
			onDispose += action;
		}

		public void Dispose() {
			onDispose();
			onDispose = null;
		}
	}
}