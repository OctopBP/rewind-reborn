using System.Collections.Generic;
using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class DoorA : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] List<PathPoint> pointsIndex;
		[SerializeField] DoorAState state = DoorAState.Closed;

		public List<PathPoint> getPointsIndex => pointsIndex;

		Model model;
		public void initialize(ITracker tracker) => model = new Model(this, tracker);
		
		public new class Model : EntityIdBehaviour.LinkedModel {
			public Model(DoorA doorA, ITracker tracker) : base(doorA, tracker) => entity
				.SetDoorA(true)
				.AddDoorAState(doorA.state)
				.AddDoorAPoints(doorA.pointsIndex)
				.AddDoorAStateListener(doorA);
		}
	}
}