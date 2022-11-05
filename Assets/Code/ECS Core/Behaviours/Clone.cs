using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class Clone : EntityLinkBehaviour<Clone.Model, PathPoint>, IPositionListener {
		protected override Model createModel(PathPoint spawnPoint) => new Model(this, spawnPoint);

		public class Model : LinkedEntityModel<GameEntity> {
			public Model(Clone clone, PathPoint spawnPoint) : base(clone.gameObject) => entity
				.with(x => x.isClone = true)
				.with(x => x.isCharacter = true)
				.with(x => x.isMovable = true)
				.with(x => x.isPathFollower = true)
				.with(e => e.AddView(clone.gameObject))
				.with(e => e.AddCurrentPoint(spawnPoint))
				.with(e => e.AddPosition(clone.transform.position))
				.with(e => e.AddPositionListener(clone));
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}