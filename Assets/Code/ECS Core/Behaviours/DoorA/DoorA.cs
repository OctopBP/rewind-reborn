using System.Collections.Generic;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class DoorA : EntityIdBehaviour {
		[SerializeField] List<PathPoint> pointsIndex;
		[SerializeField] DoorAState state = DoorAState.Closed;

		public List<PathPoint> getPointsIndex => pointsIndex;

		Model model;
		public void initialize() => model = new Model(this);
		
		public new class Model : EntityIdBehaviour.Model {
			public Model(DoorA doorA) : base(doorA) => entity
				.with(e => e.isDoorA = true)
				.with(e => e.AddDoorAState(doorA.state))
				.with(e => e.AddDoorAPoints(doorA.pointsIndex))
				.with(e => e.AddDoorAStateListener(doorA));
		}
	}
}