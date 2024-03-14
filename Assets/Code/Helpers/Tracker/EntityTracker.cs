using System;
using Entitas;

namespace Code.Helpers.Tracker
{
	public class EntityTracker : IDisposableTracker
    {
		private readonly Entity entity;

		public EntityTracker(Entity entity)
        {
			this.entity = entity;
		}
		
		public void Track() { }

		public void Dispose()
        {
			entity.Destroy();
		}

		public void Track(Action action)
        {
		}
	}
}