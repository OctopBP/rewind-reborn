using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeC : EntityIdBehaviour {
		[SerializeField] GearTypeCData data;
		[SerializeField] PathPoint pointIndex;

		Model model;
		public void initialize() {
			model = new Model(this);
			onNewState(GearTypeCState.Closed);
			setHoldMarker(false);
		}

		public new class Model : EntityIdBehaviour.Model {
			public Model(GearTypeC gearTypeC) : base(gearTypeC) => entity
				.with(e => e.isFocusable = true)
				.with(e => e.isGearTypeC = true)
				.with(e => e.isPuzzleElement = true)
				.with(e => e.AddView(gearTypeC.gameObject))
				.with(e => e.AddGearTypeCData(gearTypeC.data))
				.with(e => e.AddGearTypeCState(GearTypeCState.Closed))
				.with(e => e.AddCurrentPoint(gearTypeC.pointIndex))
				.with(e => e.AddPosition(gearTypeC.transform.position))
				.with(e => e.AddRotation(gearTypeC.transform.localEulerAngles.z))
				.with(e=> e.AddGearTypeCStateListener(gearTypeC))
				.with(e=> e.AddHoldedAtTimeListener(gearTypeC))
				.with(e=> e.AddHoldedAtTimeRemovedListener(gearTypeC));
		}
	}
}