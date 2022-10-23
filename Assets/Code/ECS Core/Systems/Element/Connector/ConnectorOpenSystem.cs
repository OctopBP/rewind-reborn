using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;
using UnityEngine;

public class ConnectorOpenSystem : IExecuteSystem {
	readonly IGroup<GameEntity> connectors;
	readonly IGroup<GameEntity> points;

	public ConnectorOpenSystem(Contexts contexts) {
		connectors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Connector, GameMatcher.PathPointsPare, GameMatcher.ConnectorState, GameMatcher.ActivateDistance
		));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.Position
		));
	}

	public void Execute() {
		foreach (var connector in connectors.GetEntities()) {
			var pathPointsPare = connector.pathPointsPare.value;
			var point1 = points.maybeFind(pathPointsPare.point1);
			var point2 = points.maybeFind(pathPointsPare.point2);

			var maybeTpl = (point1, point2).Sequence();
			maybeTpl.IfSome(tpl => {
				var (p1, p2) = tpl;
				var distance = Vector2.Distance(p1.position.value, p2.position.value);
				var state = distance > connector.activateDistance.value ? ConnectorState.Closed : ConnectorState.Opened;
				connector.ReplaceConnectorState(state);
			});
		}
	}
}