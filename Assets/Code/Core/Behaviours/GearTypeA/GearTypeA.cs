using Code.Helpers.Tracker;
using Rewind.SharedData;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class GearTypeA : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] GearTypeAData data;
		[SerializeField] PathPoint pointIndex;

		Model model;
		public void initialize(ITracker tracker) {
			model = new Model(this, tracker);
			setStatus(GearTypeAState.Closed);
			setHoldedMarker(false);
		}

		public new class Model : EntityIdBehaviour.LinkedModel {
			public Model(GearTypeA gearTypeA, ITracker tracker) : base(gearTypeA, tracker) => entity
				.SetFocusable(true)
				.SetGearTypeA(true)
				.SetPuzzleElement(true)
				.AddView(gearTypeA.gameObject)
				.AddGearTypeAData(gearTypeA.data)
				.AddGearTypeAState(GearTypeAState.Closed)
				.AddCurrentPoint(gearTypeA.pointIndex)
				.AddPosition(gearTypeA.transform.position)
				.AddRotation(gearTypeA.transform.localEulerAngles.z)
				.AddGearTypeAStateListener(gearTypeA)
				.AddHoldedAtTimeListener(gearTypeA)
				.AddHoldedAtTimeRemovedListener(gearTypeA);
		}
	}
}