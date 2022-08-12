using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeCBehaviour : ComponentBehaviour {
		[SerializeField] GearTypeCData data;
		[SerializeField] PathPointType pointIndex;

		public PathPointType point => pointIndex;

		protected override void onAwake() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isGearTypeC = true);
			entity.with(x => x.isPuzzleElement = true);

			entity.AddGearTypeCData(data);
			entity.AddGearTypeCState(GearTypeCState.Closed);

			entity.AddPointIndex(pointIndex);
			entity.AddPosition(transform.position);
			entity.AddRotation(transform.localEulerAngles.z);

			createStatus(entity);
		}
	}
}