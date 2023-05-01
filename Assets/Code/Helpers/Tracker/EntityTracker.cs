using System;
using Entitas;

namespace Code.Helpers.Tracker {
	public class EntityTracker : IDisposableTracker {
		readonly Entity entity;

		public EntityTracker(Entity entity) {
			this.entity = entity;
		}
		
		public void track() { }

		public void Dispose() {
			entity.Destroy();
		}

		public void track(Action action) {
		}
	}
}