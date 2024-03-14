using Code.Helpers.Tracker;
using Rewind.ECSCore;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Rewind.Behaviours
{
	public partial class ActionTrigger : EntityIdBehaviour, IInitWithTracker
    {
		[SerializeField, PublicAccessor] private PathPoint pointIndex;
		[SerializeField] private UnityEvent onReached;
		
		public void Initialize(ITracker tracker)
        {
			var model = new PointTrigger(pointIndex, tracker);
			model.Reached.Subscribe(_ => onReached.Invoke());
		}
	}
}