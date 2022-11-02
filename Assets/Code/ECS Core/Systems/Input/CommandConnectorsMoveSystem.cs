using System.Linq;
using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using UnityEngine;
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
		players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CurrentPoint));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.PointOpenStatus, GameMatcher.Depth
		));
		connectors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Connector, GameMatcher.PathPointsPare, GameMatcher.ConnectorState
		));
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		getMoveDirection().IfSome(direction => {
			foreach (var player in players.GetEntities()) {
				var maybePlayerPoint = points.first(player.isSamePoint);
				var maybePreviousPlayerPoint = points.first(p => p.isSamePoint(player.previousPoint.value));

				(maybePlayerPoint, maybePreviousPlayerPoint).Sequence().IfSome(tpl => {
					var (playerPoint, playerPreviousPoint) = tpl;
					var pathPointsPares = connectors
						.where(c => c.connectorState.value.isOpened())
						.Select(connector => connector.pathPointsPare.value);

					foreach (var pointsPare in pathPointsPares) {
						var maybePointEntity1 = points.first(p => p.isSamePoint(pointsPare.point1));
						var maybePointEntity2 = points.first(p => p.isSamePoint(pointsPare.point2));

						(maybePointEntity1, maybePointEntity2).Sequence().IfSome(tpl => {
							var (pointEntity1, pointEntity2) = tpl;
							var depthDiff = pointEntity1.depth.value - pointEntity2.depth.value;
							var onPoint1 = stayingOnPoint(pointEntity1);
							var onPoint2 = stayingOnPoint(pointEntity2);

							(depthDiff == 0
								? direction.fold(
									onRight: onPoint1 ? Some(pointEntity2) : None,
									onLeft: onPoint2 ? Some(pointEntity1) : None
								)
								: direction.fold(
									onUp: onPoint1 ? Some(pointEntity2) : None,
									onDown: onPoint2 ? Some(pointEntity1) : None
								)
							).IfSome(targetPoint => replacePoints(
								player, point: targetPoint.currentPoint.value,
								previousPoint: player.currentPoint.value, rewindPoint: player.previousPoint.value
							));

							bool stayingOnPoint(GameEntity pointEntity) =>
								playerPoint.isSamePoint(pointEntity) &&
								playerPreviousPoint.isSamePoint(pointEntity);
						});
					}
				});
			}
		});

		void replacePoints(GameEntity entity, PathPoint point, PathPoint previousPoint, PathPoint rewindPoint) =>
			entity
				.with(e => e.ReplaceCurrentPoint(point))
				.with(e => e.ReplacePreviousPoint(previousPoint))
				.with(e => e.ReplaceRewindPoint(rewindPoint));
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
