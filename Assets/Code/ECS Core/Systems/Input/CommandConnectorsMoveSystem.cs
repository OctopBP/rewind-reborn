using System.Linq;
using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;
using Rewind.Extensions;
using Rewind.Services;

public class CommandConnectorsMoveSystem : IExecuteSystem
{
	private readonly GameContext game;
	private readonly InputContext input;
	private readonly IGroup<GameEntity> players;
	private readonly IGroup<GameEntity> points;
	private readonly IGroup<GameEntity> connectors;
	private readonly GameEntity clock;

	public CommandConnectorsMoveSystem(Contexts contexts)
	{
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

	public void Execute()
	{
		if (clock.clockState.value.IsRewind()) return;

		input.input.GetMoveDirection().IfSome(direction =>
        {
			foreach (var player in players.GetEntities())
            {
				points.FindPointOf(player).IfSome(playerPoint =>
                {
					var pathPointsPares = connectors
						.Where(c => c.connectorState.value.IsOpened())
						.Select(connector => connector.pathPointsPare.value)
						.Select(pair => (points.FindPointOf(pair.Point1), points.FindPointOf(pair.Point2)))
						.Collect(pairTuple => pairTuple.Sequence());

					foreach (var (point1, point2) in pathPointsPares)
                    {
						var sameDepth = point1.depth.Equal(point2.depth);
						var onPoint1 = playerPoint.IsSamePoint(point1) && !player.hasPreviousPoint;
						var onPoint2 = playerPoint.IsSamePoint(point2) && !player.hasPreviousPoint;

						var conditionTargetTpl = direction.Fold(
							onRight: (condition: sameDepth && onPoint1, target: point2),
							onLeft: (condition: sameDepth && onPoint2, target: point1),
							onUp: (condition: !sameDepth && onPoint1, target: point2),
							onDown: (condition: !sameDepth && onPoint2, target: point1)
						);

						var maybeTargetPoint = conditionTargetTpl.OptionFromNullable()
							.Filter(tpl => tpl.condition)
							.Map(tpl => tpl.target);
						
						maybeTargetPoint.IfSome(targetPoint =>
                        {
							var newPoint = targetPoint.currentPoint.value;
							var currentPoint = player.currentPoint.value;

							if (game.clockEntity.clockState.value.IsRecord())
                            {
								var maybePreviousPoint = player.maybePreviousPoint_value;
								game.CreateMoveTimePoint(
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
