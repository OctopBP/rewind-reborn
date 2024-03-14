using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.Behaviours
{
	public partial class DoorA : EntityIdBehaviour, IInitWithTracker
    {
		[SerializeField, PublicAccessor] private PathPoint rightPoint;
		[SerializeField] private DoorAState state = DoorAState.Closed;

		private Model model;
		public void Initialize(ITracker tracker) => model = new Model(this, tracker);
		
		public new class Model : LinkedModel
		{
			public Model(DoorA doorA, ITracker tracker) : base(doorA, tracker) => entity
				.SetDoorA(true)
				.AddDoorAState(doorA.state)
				.AddCurrentPoint(doorA.rightPoint)
				.AddDoorAStateListener(doorA);
		}
	}
}