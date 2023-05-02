using Code.Helpers.Tracker;
using Rewind.ECSCore.Enums;
using Rewind.Infrastructure;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class LeverA : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] PathPoint pointIndex;

		Model model;
		public void initialize(ITracker tracker) {
			model = new Model(this, tracker);
			setState(LeverAState.Closed);
		}

		public new class Model : EntityIdBehaviour.Model {
			public Model(LeverA leverA, ITracker tracker) : base(leverA, tracker) => entity
				.SetIsFocusable()
				.SetIsLeverA()
				.SetIsPuzzleElement()
				.AddLeverAState(LeverAState.Closed)
				.AddCurrentPoint(leverA.pointIndex)
				.AddPosition(leverA.transform.position)
				.AddLeverAStateListener(leverA)
				.AddHoldedAtTimeListener(leverA)
				.AddHoldedAtTimeRemovedListener(leverA);
		}
	}
}