using Code.Helpers.Tracker;
using Rewind.Data;
using Rewind.ECSCore.Enums;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
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

		public new class Model : EntityIdBehaviour.Model {
			public Model(GearTypeA gearTypeA, ITracker tracker) : base(gearTypeA, tracker) => entity
				.SetIsFocusable()
				.SetIsGearTypeA()
				.SetIsPuzzleElement()
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