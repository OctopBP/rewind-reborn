using System;

namespace Code.Helpers.Tracker
{
	public class DisposableTracker : IDisposableTracker
    {
		private Action onDispose;

		public void Track(Action action)
        {
			onDispose += action;
		}

		public void Dispose()
        {
			onDispose();
			onDispose = null;
		}
	}
}