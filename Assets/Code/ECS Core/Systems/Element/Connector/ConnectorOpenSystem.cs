using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;
using UnityEngine;

public class ConnectorOpenSystem : IExecuteSystem {
	readonly IGroup<GameEntity> connectors;
	readonly IGroup<GameEntity> points;

	public ConnectorOpenSystem(Contexts contexts) {
		connectors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Connector, GameMatcher.ConnectorPoints, GameMatcher.ConnectorState,
			GameMatcher.ConnectorActivateDistance
		));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.Position
		));
	}

	public void Execute() {
		foreach (var connector in connectors.GetEntities()) {
			var point1 = points.first(p => p.isSamePoint(connector.connectorPoints.point1));
			var point2 = points.first(p => p.isSamePoint(connector.connectorPoints.point2));

			point1.IfSome(p1 => point2.IfSome(p2 => connector.ReplaceConnectorState(
				Vector2.Distance(p1.position.value, p2.position.value) > connector.connectorActivateDistance.value
					? ConnectorState.Closed : ConnectorState.Opened
			)));
		}
	}
}