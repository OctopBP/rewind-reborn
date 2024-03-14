using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using UniRx;

namespace Rewind.ECSCore
{
	public class PointTrigger : TrackedEntityModel<GameEntity>, IPointTriggerReachedListener
	{
		public readonly PathPoint Point;
		public readonly ReactiveCommand Reached = new ReactiveCommand();

		public PointTrigger(PathPoint point, ITracker tracker) : base(tracker)
		{
			Point = point;
			entity
				.SetPointTrigger(true)
				.AddCurrentPoint(point)
				.AddPointTriggerReachedListener(this);
		}

		public void OnPointTriggerReached(GameEntity _) => Reached.Execute();
	}
}