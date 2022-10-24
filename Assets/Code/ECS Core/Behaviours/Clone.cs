using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class Clone : EntityLinkBehaviour<Clone.Model, PathPointType>, IPositionListener {
		protected override Model createModel(PathPointType spawnPoint) => new Model(this, spawnPoint);

		public class Model : LinkedEntityModel<GameEntity> {
			public Model(Clone clone, PathPointType spawnPoint) : base(clone.gameObject) => entity
				.with(x => x.isClone = true)
				.with(x => x.isCharacter = true)
				.with(x => x.isMovable = true)
				.with(x => x.isPathFollower = true)
				.with(e => e.AddView(clone.gameObject))
				.with(e => e.AddPointIndex(spawnPoint))
				.with(e => e.AddPreviousPointIndex(spawnPoint))
				.with(e => e.AddPosition(clone.transform.position))
				.with(e => e.AddMoveState(MoveState.Standing))
				.with(e => e.AddPositionListener(clone));
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}