using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.ECSCore {
	public class CharacterBehaviour : SelfInitializedView {
		[SerializeField] float speed;
		[SerializeField] PathPointType spawnPoint;

		protected override void onAwake() {
			base.onAwake();
			setupCharacter();
		}

		void setupCharacter() {
			entity.with(x => x.isPlayer = true);
			entity.with(x => x.isCharacter = true);
			entity.with(x => x.isMovable = true);
			entity.with(x => x.isPathFollower = true);
			entity.AddPathFollowerSpeed(speed);
			entity.AddPointIndex(spawnPoint);
			entity.AddPreviousPointIndex(spawnPoint);
			entity.AddPosition(transform.position);
			entity.AddMoveState(MoveState.Standing);
		}
	}
}