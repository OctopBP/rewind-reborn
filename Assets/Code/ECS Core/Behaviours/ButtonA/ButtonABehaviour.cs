using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class ButtonABehaviour : ComponentBehaviour {
		[SerializeField] PathPointType pointIndex;

		public PathPointType getPointIndex => pointIndex;

		protected override void initialize() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isButtonA = true);
			entity.with(x => x.isPuzzleElement = true);
			
			entity.AddButtonAState(ButtonAState.Closed);
			entity.AddPointIndex(pointIndex);
			entity.AddPosition(transform.position);
		}
	}
}