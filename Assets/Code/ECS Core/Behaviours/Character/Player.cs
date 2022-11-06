using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;
using UnityEngine;

namespace Rewind.ECSCore {
	public class Player : EntityLinkBehaviour<Player.Model, PathPoint>, IPositionListener {
		[SerializeField] CharacterAnimator animator;
		
		protected override Model createModel(PathPoint spawnPoint) => new Model(this, spawnPoint);

		public class Model : LinkedEntityModel<GameEntity> {
			public Model(Player backing, PathPoint spawnPoint) : base(backing.gameObject) => entity
				.with(x => x.isPlayer = true)
				.with(x => x.isCharacter = true)
				.with(x => x.isMovable = true)
				.with(x => x.isPathFollower = true)
				.with(e => e.AddView(backing.gameObject))
				.with(e => e.AddCurrentPoint(spawnPoint))
				.with(e => e.AddPosition(backing.transform.position))
				.with(e => e.AddCharacterState(CharacterState.Idle))
				.with(e => e.AddMoveState(MoveState.None))
				.with(e => e.AddPositionListener(backing))
				.with(e => e.AddCharacterStateListener(backing.animator));
		}

		public void OnPosition(GameEntity _, Vector2 value) => transform.position = value;
	}
}