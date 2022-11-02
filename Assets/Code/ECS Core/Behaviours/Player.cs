using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class Player : EntityLinkBehaviour<Player.Model, PathPoint>, IPositionListener {
		protected override Model createModel(PathPoint spawnPoint) => new Model(this, spawnPoint);

		public class Model : LinkedEntityModel<GameEntity> {
			public Model(Player player, PathPoint spawnPoint) : base(player.gameObject) => entity
				.with(x => x.isPlayer = true)
				.with(x => x.isCharacter = true)
				.with(x => x.isMovable = true)
				.with(x => x.isPathFollower = true)
				.with(e => e.AddView(player.gameObject))
				.with(e => e.AddCurrentPoint(spawnPoint))
				.with(e => e.AddPosition(player.transform.position))
				.with(e => e.AddMoveState(MoveState.Standing))
				.with(e => e.AddPositionListener(player));
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}