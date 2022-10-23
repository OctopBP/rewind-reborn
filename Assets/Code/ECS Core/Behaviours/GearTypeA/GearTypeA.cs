using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeA : MonoBehaviour {
		[SerializeField] GearTypeAData data;
		[SerializeField] PathPointType pointIndex;

		Model model;
		public void initialize() {
			model = new Model(this);
			setStatus(GearTypeAState.Closed);
			setHoldedMarker(false);
		}

		public class Model : EntityModel<GameEntity> {
			public Model(GearTypeA gearTypeA) => entity
				.with(e => e.isFocusable = true)
				.with(e => e.isGearTypeA = true)
				.with(e => e.isPuzzleElement = true)
				.with(e => e.AddGearTypeAData(gearTypeA.data))
				.with(e => e.AddGearTypeAState(GearTypeAState.Closed))
				.with(e => e.AddPointIndex(gearTypeA.pointIndex))
				.with(e => e.AddPosition(gearTypeA.transform.position))
				.with(e => e.AddRotation(gearTypeA.transform.localEulerAngles.z))
				.with(e => e.AddGearTypeAStateListener(gearTypeA))
				.with(e => e.AddHoldedAtTimeListener(gearTypeA))
				.with(e => e.AddHoldedAtTimeRemovedListener(gearTypeA));
		}
	}
}