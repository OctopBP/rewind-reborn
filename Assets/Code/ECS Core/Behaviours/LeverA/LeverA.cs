using Code.Helpers.Tracker;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
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
				.with(e => e.isFocusable = true)
				.with(e => e.isLeverA = true)
				.with(e => e.isPuzzleElement = true)
				.with(e => e.AddLeverAState(LeverAState.Closed))
				.with(e => e.AddCurrentPoint(leverA.pointIndex))
				.with(e => e.AddPosition(leverA.transform.position))
				.with(e => e.AddLeverAStateListener(leverA))
				.with(e => e.AddHoldedAtTimeListener(leverA))
				.with(e => e.AddHoldedAtTimeRemovedListener(leverA));
		}
	}
}