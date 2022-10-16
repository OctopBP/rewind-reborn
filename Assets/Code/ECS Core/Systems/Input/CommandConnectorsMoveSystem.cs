using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.Services;
using static LanguageExt.Prelude;

public class CommandConnectorsMoveSystem : IExecuteSystem {
	readonly InputContext input;
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> connectors;
	readonly GameEntity clock;

	public CommandConnectorsMoveSystem(Contexts contexts) {
		input = contexts.input;
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.PointIndex));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.PointOpenStatus, GameMatcher.Depth
		));
		connectors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Connector, GameMatcher.ConnectorPoints, GameMatcher.ConnectorState
		));
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		getMoveDirection().IfSome(direction => {
			foreach (var player in players.GetEntities()) {
				points.first(player.isSamePoint).IfSome(playerPoint => {
					points.first(p => p.isSamePoint(player.previousPointIndex.value)).IfSome(playerPreviousPoint => {
						foreach (var connector in connectors.where(c => c.connectorState.value.isOpened())) {
							var point1 = connector.connectorPoints.point1;
							var point2 = connector.connectorPoints.point2;

							points.first(p => p.isSamePoint(point1)).IfSome(pointEntity1 =>
								points.first(p => p.isSamePoint(point2)).IfSome(pointEntity2 => {
									var depthDiff = pointEntity1.depth.value - pointEntity2.depth.value;
									var onPoint1 = stayingOnPoint(pointEntity1);
									var onPoint2 = stayingOnPoint(pointEntity2);

									(depthDiff == 0
										? direction.map(
											onRight: onPoint1 ? Some(pointEntity2) : None,
											onLeft: onPoint2 ? Some(pointEntity1) : None
										)
										: direction.map(
											onUp: onPoint1 ? Some(pointEntity2) : None,
											onDown: onPoint2 ? Some(pointEntity1) : None
										)
									).IfSome(targetPoint => {
										player.ReplaceRewindPointIndex(player.previousPointIndex.value);
										player.ReplacePreviousPointIndex(player.pointIndex.value);
										player.ReplacePointIndex(targetPoint.pointIndex.value);
									});

									bool stayingOnPoint(GameEntity pointEntity) =>
										playerPoint.isSamePoint(pointEntity) &&
										playerPreviousPoint.isSamePoint(pointEntity);
								})
							);
						}
					});
				});
			}
		});
	}

	Option<MoveDirection> getMoveDirection() {
		var inputService = input.input.value;

		if (inputService.getMoveRightButton()) return Some(MoveDirection.Right);
		if (inputService.getMoveLeftButton()) return Some(MoveDirection.Left);
		if (inputService.getMoveUpButton()) return Some(MoveDirection.Up);
		if (inputService.getMoveDownButton()) return Some(MoveDirection.Down);

		return None;
	}
}
