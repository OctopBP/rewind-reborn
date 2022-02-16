using Rewind.Extensions;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.ECSCore {
	public class CharacterBehaviour : SelfInitializedView {
		[SerializeField] float speed;

		protected override void onAwake() {
			base.onAwake();
			setupCharacter();
		}

		void setupCharacter() {
			entity.with(x => x.isCharacter = true);
			entity.with(x => x.isMovable = true);
			entity.with(x => x.isPathFollower = true);
			entity.AddPathFollowerSpeed(speed);
			entity.AddPathIndex(0);
			entity.AddPointIndex(0);
			entity.AddPreviousPathIndex(0);
			entity.AddPreviousPointIndex(0);
			entity.AddPosition(transform.position);
		}
	}
}