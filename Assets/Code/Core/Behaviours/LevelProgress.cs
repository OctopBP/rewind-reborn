using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using Rewind.LogicBuilder;
using UniRx;

namespace Rewind.Core
{
	public class LevelProgress : TrackedEntityModel<GameEntity>, ILevelProgressListener
	{
		public readonly ReactiveProperty<int> Progress = new();
		
		public LevelProgress(ITracker tracker, ConditionGroup progressCondition) : base(tracker) => entity
			.AddLevelProgress(0)
			.AddLevelProgressListener(this)
			.AddConditionGroup(progressCondition);

		public void OnLevelProgress(GameEntity _, int value) => Progress.Value = value;
	}
}