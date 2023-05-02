using Code.Helpers.Tracker;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeC : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] GearTypeCData data;
		[SerializeField] PathPoint pointIndex;

		Model model;
		public void initialize(ITracker tracker) {
			model = new Model(this, tracker);
			onNewState(GearTypeCState.Closed);
			setHoldMarker(false);
		}

		public new class Model : EntityIdBehaviour.Model {
			public Model(GearTypeC gearTypeC, ITracker tracker) : base(gearTypeC, tracker) => entity
				.SetIsFocusable()
				.SetIsGearTypeC()
				.SetIsPuzzleElement()
				.AddView(gearTypeC.gameObject)
				.AddGearTypeCData(gearTypeC.data)
				.AddGearTypeCState(GearTypeCState.Closed)
				.AddCurrentPoint(gearTypeC.pointIndex)
				.AddPosition(gearTypeC.transform.position)
				.AddRotation(gearTypeC.transform.localEulerAngles.z)
				.AddGearTypeCStateListener(gearTypeC)
				.AddHoldedAtTimeListener(gearTypeC)
				.AddHoldedAtTimeRemovedListener(gearTypeC);
		}
	}
}