using Entitas;

namespace Rewind.ViewListeners
{
	public interface IViewController
	{
		IViewController InitializeView(GameContext @in, IEntity @with);
		void Destroy();
	}
}