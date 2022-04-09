using System;
using Code.Base;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using Rewind.ViewListeners;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rewind.Behaviours {
	public class ConnectorBehaviour : SelfInitializedView, IEventListener, IConnectorStateListener, IStatusValue {
		[Space(10), SerializeField] ConnectorDirection direction;
		
		[LabelText("@" + nameof(label1)), SerializeField] PathPointType point1;
		[LabelText("@" + nameof(label2)), SerializeField] PathPointType point2;
		
		[PropertyOrder(1), Space(10), SerializeField] float activateDistance;

		[Button] void swapPoints() => (point1, point2) = (point2, point1);

		enum ConnectorDirection { LeftToRight, RightToLeft, TopToBottom, BottomToTop }

		string label1 => direction switch {
			ConnectorDirection.LeftToRight => "Left",
			ConnectorDirection.RightToLeft => "Right",
			ConnectorDirection.TopToBottom => "Top",
			ConnectorDirection.BottomToTop => "Bottom",
		};

		string label2 => direction switch {
			ConnectorDirection.LeftToRight => "Right",
			ConnectorDirection.RightToLeft => "Left",
			ConnectorDirection.TopToBottom => "Bottom",
			ConnectorDirection.BottomToTop => "Top",
		};

		public float statusValue => entity.connectorState.value switch {
			ConnectorState.Closed => 0,
			ConnectorState.Opened => 1,
			_ => throw new ArgumentOutOfRangeException()
		};

		public PathPointType getPoint1 => point1;
		public PathPointType getPoint2 => point2;
		public float getActivateDistance => activateDistance;

		ConnectorState state { get; set; } = ConnectorState.Closed;

		protected override void onAwake() {
			base.onAwake();
			setupConnector();
		}

		void setupConnector() {
			entity.with(x => x.isConnector = true);
			entity.AddConnectorPoints(point1, point2);
			entity.AddConnectorActivateDistance(activateDistance);
			entity.AddConnectorState(state);
		}

		public void registerListeners(IEntity _) => entity.AddConnectorStateListener(this);
		public void unregisterListeners(IEntity _) => entity.RemoveConnectorStateListener(this);

		public void OnConnectorState(GameEntity _, ConnectorState value) => state = value;
	}
}