using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.Behaviours {
	public partial class LeverABehaviour : ComponentBehaviour {
		[SerializeField] PathPointType pointIndex;

		public PathPointType getPointIndex => pointIndex;

		protected override void onAwake() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isLeverA = true);
			entity.with(x => x.isPuzzleElement = true);
			
			entity.AddLeverAState(LeverAState.Closed);

			entity.AddPointIndex(pointIndex);
			entity.AddPosition(transform.position);

			createStatus(entity);
		}
	}
}