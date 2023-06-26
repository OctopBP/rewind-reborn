using System.Linq;
using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;
using Rewind.Extensions;
using Rewind.Services;

public class CommandConnectorsMoveSystem : IExecuteSystem {
	readonly GameContext game;
	readonly InputContext input;
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> connectors;
	readonly GameEntity clock;

	public CommandConnectorsMoveSystem(Contexts contexts) {
		game = contexts.game;
		input = contexts.input;
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CurrentPoint));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.LeftPathDirectionBlocks, GameMatcher.Depth
		));
		connectors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Connector, GameMatcher.PathPointsPare, GameMatcher.ConnectorState
		));
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		input.input.getMoveDirection().IfSome(direction => {
			foreach (var player in players.GetEntities()) {
				points.findPointOf(player).IfSome(playerPoint => {
					var pathPointsPares = connectors
						.where(c => c.connectorState.value.isOpened())
						.Select(connector => connector.pathPointsPare.value)
						.Select(pair => (points.findPointOf(pair.point1), points.findPointOf(pair.point2)))
						.Select(pairTuple => pairTuple.Sequence())
						.Somes();
					
					foreach (var (point1, point2) in pathPointsPares) {
						var sameDepth = point1.depth.equal(point2.depth);
						var onPoint1 = playerPoint.isSamePoint(point1) && !player.hasPreviousPoint;
						var onPoint2 = playerPoint.isSamePoint(point2) && !player.hasPreviousPoint;

						var conditionTargetTpl = direction.fold(
							onRight: (condition: sameDepth && onPoint1, target: point2),
							onLeft: (condition: sameDepth && onPoint2, target: point1),
							onUp: (condition: !sameDepth && onPoint1, target: point2),
							onDown: (condition: !sameDepth && onPoint2, target: point1)
						);

						var maybeTargetPoint = conditionTargetTpl.optionFromNullable()
							.Filter(tpl => tpl.condition)
							.Map(tpl => tpl.target);
						
						maybeTargetPoint.IfSome(targetPoint => {
							var newPoint = targetPoint.currentPoint.value;
							var currentPoint = player.currentPoint.value;

							if (game.clockEntity.clockState.value.isRecord()) {
								var maybePreviousPoint = player.maybePreviousPoint_value;
								game.createMoveTimePoint(
									currentPoint: newPoint, previousPoint: currentPoint,
									rewindPoint: maybePreviousPoint.IfNone(currentPoint)
								);
							}

							player
								.ReplaceCurrentPoint(newPoint)
								.ReplacePreviousPoint(currentPoint);
						});
					}
				});
			}
		});
	}
}
