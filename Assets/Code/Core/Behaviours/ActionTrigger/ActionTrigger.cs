using Code.Helpers.Tracker;
using Rewind.ECSCore;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace Rewind.Behaviours {
	public partial class ActionTrigger : EntityIdBehaviour, IInitWithTracker {
		[SerializeField, PublicAccessor] PathPoint pointIndex;
		[SerializeField] UnityEvent onReached;
		
		public void initialize(ITracker tracker) {
			var model = new PointTrigger(pointIndex, tracker);
			model.reached.Subscribe(_ => onReached.Invoke());
		}
	}
}