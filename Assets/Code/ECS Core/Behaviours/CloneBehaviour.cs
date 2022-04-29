using Rewind.Extensions;

namespace Rewind.ECSCore {
	public class CloneBehaviour : CharacterBehaviour {
		public override void init(PathPointType spawnPoint, float speed) {
			base.init(spawnPoint, speed);
			entity.with(x => x.isClone = true);
		}
	}
}