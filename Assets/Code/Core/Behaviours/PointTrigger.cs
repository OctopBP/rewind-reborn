using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using UniRx;

namespace Rewind.ECSCore {
	public class PointTrigger : TrackedEntityModel<GameEntity>, IPointTriggerReachedListener {
		public readonly PathPoint point;
		public readonly ReactiveCommand reached = new ReactiveCommand();

		public PointTrigger(PathPoint point, ITracker tracker) : base(tracker) {
			this.point = point;
			entity
				.SetPointTrigger(true)
				.AddCurrentPoint(point)
				.AddPointTriggerReachedListener(this);
		}

		public void OnPointTriggerReached(GameEntity _) => reached.Execute();
	}
}