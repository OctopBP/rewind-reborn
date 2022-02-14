using Entitas;

namespace Rewind.ViewListeners {
	public interface IViewController {
		IViewController initializeView(GameContext @in, IEntity @with);
		void destroy();
		GameEntity entity { get; }
	}
}