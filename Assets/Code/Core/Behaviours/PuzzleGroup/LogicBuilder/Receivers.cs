using System;
using Rewind.Behaviours;
using Rewind.SharedData;
using UnityEngine;

namespace Rewind.LogicBuilder {
	[Serializable]
	public class DoorAValueReceiver : IPuzzleValueReceiver {
		[SerializeField] DoorA door;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isDoorA && e.hasId && e.id.value == door.id.guid;
		}

		public void receiveValue(GameEntity entity, float value) {
			entity.ReplaceDoorAState(value > 0 ? DoorAState.Opened : DoorAState.Closed);
		}
	}
	
	[Serializable]
	public class PlatformAValueReceiver : IPuzzleValueReceiver {
		[SerializeField] PlatformA platform;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isPlatformA && e.hasId && e.id.value == platform.id.guid;
		}

		public void receiveValue(GameEntity entity, float value) {
			entity.ReplacePlatformAState(value > 0 ? PlatformAState.Active : PlatformAState.NotActive);
		}
	}

	[Serializable]
	public class PathConnectorValueReceiver : IPuzzleValueReceiver {
		[SerializeField] PathConnector pathConnector;

		public Func<GameEntity, bool> entityFilter() {
			return e => e.isConnector && e.hasId && e.id.value == pathConnector.id.guid;
		}

		public void receiveValue(GameEntity entity, float value) {
			entity.ReplaceConnectorState(value > 0 ? ConnectorState.Opened : ConnectorState.Closed);
		}
	}
}
