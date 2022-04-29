using Rewind.Extensions;

namespace Rewind.ECSCore {
	public class PlayerBehaviour : CharacterBehaviour {
		public override void init(PathPointType spawnPoint, float speed) {
			base.init(spawnPoint, speed);
			entity.with(x => x.isPlayer = true);
		}
	}
}