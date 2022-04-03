using System;
using Code.Base;
using Entitas;
using Rewind.ECSCore;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using UnityEngine;

namespace Rewind.Behaviours {
	public class ConnectorBehaviour : SelfInitializedView, IEventListener, IConnectorStateListener, IStatusValue {
		[SerializeField] PathBehaviour path;
		[SerializeField] PathPointType point;
		[SerializeField] float activateDistance;

		public float statusValue => entity.connectorState.value switch {
			ConnectorState.Closed => 0,
			ConnectorState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};

		public PathPointType point1 => point;
		public PathPointType point2 => new(path.id, 0);

		public ConnectorState state { get; private set; } = ConnectorState.Closed;

		protected override void onAwake() {
			base.onAwake();
			setupConnector();
		}

		void setupConnector() {
			entity.with(x => x.isConnector = true);
			entity.AddConnectorPoints(point, point2);
			entity.AddConnectorActivateDistance(activateDistance);
			entity.AddConnectorState(state);
		}

		public void registerListeners(IEntity _) => entity.AddConnectorStateListener(this);
		public void unregisterListeners(IEntity _) => entity.RemoveConnectorStateListener(this);

		public void OnConnectorState(GameEntity _, ConnectorState value) => state = value;
	}
}