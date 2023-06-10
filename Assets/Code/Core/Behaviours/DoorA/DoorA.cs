using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class DoorA : EntityIdBehaviour, IInitWithTracker {
		[SerializeField, PublicAccessor] PathPoint rightPoint;
		[SerializeField] DoorAState state = DoorAState.Closed;

		Model model;
		public void initialize(ITracker tracker) => model = new Model(this, tracker);
		
		public new class Model : LinkedModel {
			public Model(DoorA doorA, ITracker tracker) : base(doorA, tracker) => entity
				.SetDoorA(true)
				.AddDoorAState(doorA.state)
				.AddCurrentPoint(doorA.rightPoint)
				.AddDoorAStateListener(doorA);
		}
	}
}