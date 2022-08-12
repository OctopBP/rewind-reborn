using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeABehaviour : ComponentBehaviour {
		[SerializeField] GearTypeAData data;
		[SerializeField] PathPointType pointIndex;

		public PathPointType point => pointIndex;

		protected override void onAwake() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isGearTypeA = true);
			entity.with(x => x.isPuzzleElement = true);

			entity.AddGearTypeAData(data);
			entity.AddGearTypeAState(GearTypeAState.Closed);

			entity.AddPointIndex(pointIndex);
			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);
		}
	}
}