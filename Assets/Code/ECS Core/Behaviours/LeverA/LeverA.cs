using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class LeverA : MonoBehaviour {
		[SerializeField] PathPointType pointIndex;

		Model model;
		public void initialize() {
			model = new Model(this);
			setState(LeverAState.Closed);
		}

		public class Model : EntityModel<GameEntity> {
			public Model(LeverA leverA) => entity
				.with(e => e.isFocusable = true)
				.with(e => e.isLeverA = true)
				.with(e => e.isPuzzleElement = true)
				.with(e => e.AddLeverAState(LeverAState.Closed))
				.with(e => e.AddPointIndex(leverA.pointIndex))
				.with(e => e.AddPosition(leverA.transform.position))
				.with(e => e.AddLeverAStateListener(leverA))
				.with(e => e.AddHoldedAtTimeListener(leverA))
				.with(e => e.AddHoldedAtTimeRemovedListener(leverA));
		}
	}
}