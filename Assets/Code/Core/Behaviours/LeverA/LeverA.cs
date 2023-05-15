using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class LeverA : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] PathPoint pointIndex;

		Model model;
		public void initialize(ITracker tracker) {
			model = new Model(this, tracker);
			setState(LeverAState.Closed);
		}

		public new class Model : EntityIdBehaviour.LinkedModel {
			public Model(LeverA leverA, ITracker tracker) : base(leverA, tracker) => entity
				.SetFocusable(true)
				.SetLeverA(true)
				.SetPuzzleElement(true)
				.AddLeverAState(LeverAState.Closed)
				.AddCurrentPoint(leverA.pointIndex)
				.AddPosition(leverA.transform.position)
				.AddLeverAStateListener(leverA)
				.AddHoldedAtTimeListener(leverA)
				.AddHoldedAtTimeRemovedListener(leverA);
		}
	}
}