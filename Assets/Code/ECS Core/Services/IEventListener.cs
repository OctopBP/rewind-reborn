namespace Rewind.Services {
	public interface IEventListener {
		void registerListeners();
		void unregisterListeners();
	}
}