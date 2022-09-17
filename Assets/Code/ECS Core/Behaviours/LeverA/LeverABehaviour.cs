using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

interface IConvertable {
	void convert(GameEntity entity);
}

namespace Rewind.Behaviours {
	public partial class LeverABehaviour : EntityIdBehaviour, IConvertable {
		[SerializeField] PathPointType pointIndex;

		public PathPointType getPointIndex => pointIndex;

		protected override void initialize() {
			entity.with(x => x.isFocusable = true);
			entity.with(x => x.isLeverA = true);
			entity.with(x => x.isPuzzleElement = true);
			
			entity.AddLeverAState(LeverAState.Closed);

			entity.AddPointIndex(pointIndex);
			entity.AddPosition(transform.position);

			createStatus(entity);
		}

		public void convert(GameEntity entity) {
			entity.isFocusable = true;
		}
	}
}