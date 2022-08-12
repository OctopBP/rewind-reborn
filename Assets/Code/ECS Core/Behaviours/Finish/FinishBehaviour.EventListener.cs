using Rewind.Services;

namespace Rewind.Behaviours {
	public partial class FinishBehaviour : IEventListener, IFinishReachedListener {
		public void registerListeners() => entity.AddFinishReachedListener(this);
		public void unregisterListeners() => entity.RemoveFinishReachedListener(this);
		public void OnFinishReached(GameEntity _) => reached.Value = true;
	}
}