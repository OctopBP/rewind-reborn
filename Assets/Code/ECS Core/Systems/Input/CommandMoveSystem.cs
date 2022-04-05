using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;

public class CommandMoveSystem : IExecuteSystem {
	readonly InputContext input;
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> connectors;
	readonly GameEntity clock;

	public CommandMoveSystem(Contexts contexts) {
		input = contexts.input;
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.PointIndex));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.PointOpenStatus
		));
		connectors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Connector, GameMatcher.ConnectorPoints, GameMatcher.ConnectorState
		));
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		getMoveDirection().IfSome(direction => {
			foreach (var player in players.GetEntities()) {
				var nextPointIndex = player.pointIndex.value.index + direction.intValue();

				if ((nextPointIndex - player.previousPointIndex.value.index).abs() < 2 &&
				    player.pointIndex.value.pathId == player.previousPointIndex.value.pathId
				) {
					var currentPoint = points.first(player.isSamePoint);
					var canMoveFromThisPoint = currentPoint
						.Some(p => direction.map(
							onLeft: p.pointOpenStatus.value.isOpenLeft(),
							onRight: p.pointOpenStatus.value.isOpenRight()
						))
						.None(() => false);

					var canMoveToNextPoint = points
						.first(p => p.isSamePoint(player.pointIndex.value.pathId, nextPointIndex))
						.Some(p => direction.map(
							onLeft: p.pointOpenStatus.value.isOpenRight(),
							onRight: p.pointOpenStatus.value.isOpenLeft()
						))
						.None(() => false);

					if (canMoveFromThisPoint && canMoveToNextPoint) {
						player.ReplaceRewindPointIndex(player.previousPointIndex.value);
						player.ReplacePreviousPointIndex(player.pointIndex.value);
						player.ReplacePointIndex(new(player.pointIndex.value.pathId, nextPointIndex));
					} else {
						currentPoint.IfSome(point => {
							foreach (var connector in connectors.where(c => c.connectorState.value.isOpened())) {
								var point1 = connector.connectorPoints.pointLeft;
								var point2 = connector.connectorPoints.pointRight;

								var maybeTargetPoint = point.isSamePoint(point1) && direction.isRight()
									? point2
									: point.isSamePoint(point2) && direction.isLeft()
										? point1
										: Option<PathPointType>.None;

								maybeTargetPoint.IfSome(targetPoint => {
									player.ReplaceRewindPointIndex(player.previousPointIndex.value);
									player.ReplacePreviousPointIndex(player.pointIndex.value);
									player.ReplacePointIndex(targetPoint);
								});
							}
						});
					}
				}
			}
		});
	}

	Option<MoveDirection> getMoveDirection() {
		if (input.input.value.getMoveRightButton())
			return MoveDirection.Right;

		if (input.input.value.getMoveLeftButton())
			return MoveDirection.Left;

		return Option<MoveDirection>.None;
	}
}
