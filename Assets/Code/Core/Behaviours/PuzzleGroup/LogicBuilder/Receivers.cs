using System;
using Rewind.Behaviours;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.LogicBuilder
{
	[Serializable]
	public class DoorAValueReceiver : IPuzzleValueReceiver
	{
		[SerializeField] private DoorA door;

		public Func<GameEntity, bool> EntityFilter()
		{
			return e => e.isDoorA && e.hasId && e.id.value == door.id.Guid;
		}

		public void ReceiveValue(GameEntity entity, float value)
        {
			entity.ReplaceDoorAState(value > 0 ? DoorAState.Opened : DoorAState.Closed);
		}
	}
	
	[Serializable]
	public class PlatformAValueReceiver : IPuzzleValueReceiver
    {
		[SerializeField] private PlatformA platform;

		public Func<GameEntity, bool> EntityFilter()
        {
			return e => e.isPlatformA && e.hasId && e.id.value == platform.id.Guid;
		}

		public void ReceiveValue(GameEntity entity, float value)
        {
			entity.ReplacePlatformAState(value > 0 ? PlatformAState.Active : PlatformAState.NotActive);
		}
	}

	[Serializable]
	public class PathConnectorValueReceiver : IPuzzleValueReceiver
    {
		[SerializeField] private PathConnector pathConnector;

		public PathConnectorValueReceiver(PathConnector pathConnector)
        {
			this.pathConnector = pathConnector;
		}
		
		public Func<GameEntity, bool> EntityFilter()
        {
			return e => e.isConnector && e.hasId && e.id.value == pathConnector.id.Guid;
		}

		public void ReceiveValue(GameEntity entity, float value)
        {
			entity.ReplaceConnectorState(value > 0 ? ConnectorState.Opened : ConnectorState.Closed);
		}
	}
}
