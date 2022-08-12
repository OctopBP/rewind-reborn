using System.Collections.Generic;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class DoorABehaviour : ComponentBehaviour {
		[SerializeField] List<PathPointType> pointsIndex;
		[SerializeField] DoorAState state = DoorAState.Closed;

		public List<PathPointType> getPointsIndex => pointsIndex;

		protected override void onAwake() {
			entity.with(x => x.isDoorA = true);
			entity.AddDoorAState(state);
			entity.AddDoorAPoints(pointsIndex);
		}
	}
}