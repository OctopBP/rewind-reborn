using Entitas;

namespace Rewind.Services {
	public interface IEventListener {
		void registerListeners(IEntity entity);
		void unregisterListeners(IEntity with);
	}
}