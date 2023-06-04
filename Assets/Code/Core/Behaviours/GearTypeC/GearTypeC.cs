using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using Rewind.SharedData;
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

		public new class Model : EntityIdBehaviour.LinkedModel {
			public Model(GearTypeC gearTypeC, ITracker tracker) : base(gearTypeC, tracker) => entity
				.SetFocusable(true)
				.SetGearTypeC(true)
				.SetPuzzleElement(true)
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