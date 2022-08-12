using Rewind.Infrastructure;

namespace Rewind.ViewListeners {
	public class SelfInitializedView : ComponentBehaviour {
		protected override void onAwake() {
			entity.AddView(gameObject);
		}
	}
}