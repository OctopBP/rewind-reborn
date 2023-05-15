using Code.Helpers.Tracker;
using Rewind.Infrastructure;
using Rewind.SharedData;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class ButtonA : EntityIdBehaviour, IInitWithTracker {
		[SerializeField] PathPoint pointIndex;

		Model model;
		public void initialize(ITracker tracker) => model = new Model(this, tracker);

		public new class Model : EntityIdBehaviour.LinkedModel, IButtonAStateListener {
			public ButtonAState state = ButtonAState.Closed;

			public Model(ButtonA buttonA, ITracker tracker) : base(buttonA, tracker) {
				tracker.track(() => entity.Destroy());
				
				entity
					.SetButtonA(true)
					.SetFocusable(true)
					.SetPuzzleElement(true)
					.AddButtonAState(ButtonAState.Closed)
					.AddCurrentPoint(buttonA.pointIndex)
					.AddButtonAStateListener(this) // This is needed for StatusValue
					.AddButtonAStateListener(buttonA)
					.AddHoldedAtTimeListener(buttonA)
					.AddHoldedAtTimeRemovedListener(buttonA);
			}

			public void OnButtonAState(GameEntity _, ButtonAState value) => state = value;
		}
	}
}