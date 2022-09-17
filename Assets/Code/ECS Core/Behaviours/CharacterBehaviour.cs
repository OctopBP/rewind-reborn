using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Infrastructure;

namespace Rewind.ECSCore {
	public class CharacterBehaviour : ComponentBehaviour {
		protected GameEntity entity;
		
		// todo: init orders???
		public virtual void init(PathPointType spawnPoint, float speed) {
			setupCharacter(spawnPoint, speed);
		}

		protected override void initialize() {
			this.entity = entity;
		}

		void setupCharacter(PathPointType spawnPoint, float speed) {
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