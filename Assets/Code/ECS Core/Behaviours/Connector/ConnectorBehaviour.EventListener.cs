using Rewind.ECSCore.Enums;
using Rewind.Services;

namespace Rewind.Behaviours {
	public partial class ConnectorBehaviour : IEventListener, IConnectorStateListener {
		public void registerListeners() => entity.AddConnectorStateListener(this);
		public void unregisterListeners() => entity.RemoveConnectorStateListener(this);

		public void OnConnectorState(GameEntity _, ConnectorState value) => state = value;
	}
}