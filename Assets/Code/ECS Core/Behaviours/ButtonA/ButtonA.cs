using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class ButtonA : EntityIdBehaviour {
		[SerializeField] PathPoint pointIndex;

		Model model;
		public void initialize() => model = new Model(this);

		public new class Model : EntityIdBehaviour.Model, IButtonAStateListener {
			public ButtonAState state = ButtonAState.Closed;

			public Model(ButtonA buttonA) : base(buttonA) => entity
				.with(e => e.isButtonA = true)
				.with(e => e.isFocusable = true)
				.with(e => e.isPuzzleElement = true)
				.with(e => e.AddButtonAState(ButtonAState.Closed))
				.with(e => e.AddCurrentPoint(buttonA.pointIndex))
				.with(e => e.AddButtonAStateListener(this)) // This is needed for StatusValue
				.with(e => e.AddButtonAStateListener(buttonA))
				.with(e => e.AddHoldedAtTimeListener(buttonA))
				.with(e => e.AddHoldedAtTimeRemovedListener(buttonA));

			public void OnButtonAState(GameEntity _, ButtonAState value) => state = value;
		}
	}
}