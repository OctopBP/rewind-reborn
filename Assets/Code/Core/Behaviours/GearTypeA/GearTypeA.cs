using Code.Helpers.Tracker;
using Rewind.SharedData;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours
{
	public partial class GearTypeA : EntityIdBehaviour, IInitWithTracker
	{
		[SerializeField] private GearTypeAData data;
		[SerializeField] private PathPoint pointIndex;

		private Model model;
		public void Initialize(ITracker tracker)
		{
			model = new Model(this, tracker);
			SetStatus(GearTypeAState.Closed);
			SetHoldedMarker(false);
		}

		public new class Model : LinkedModel
		{
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