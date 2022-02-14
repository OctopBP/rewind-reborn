using Rewind.Infrastructure;
using Rewind.Services;

namespace Rewind.ViewListeners {
	public class SelfInitializedView : EntityBehaviour {
		protected override void onAwake() {
			base.onAwake();

			var newEntity = game.CreateEntity();
			viewController.initializeView(game, newEntity);
			gameObject.registerListeners(newEntity);
		}
	}
}