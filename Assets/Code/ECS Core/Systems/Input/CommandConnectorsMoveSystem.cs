using Entitas;
using LanguageExt;
using Rewind.ECSCore.Enums;
using Rewind.Extensions;
using Rewind.Services;
using UnityEngine;

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
					foreach (var connector in connectors.where(c => c.connectorState.value.isOpened())) {
						var point1 = connector.connectorPoints.pointLeft;
						var point2 = connector.connectorPoints.pointRight;

						points.first(p => p.isSamePoint(point1)).IfSome(pointEntity1 =>
							points.first(p => p.isSamePoint(point2)).IfSome(pointEntity2 => {
								var depthDiff = pointEntity1.depth.value - pointEntity2.depth.value;
								var onPoint1 = playerPoint.isSamePoint(pointEntity1);
								var onPoint2 = playerPoint.isSamePoint(pointEntity2);

								(depthDiff == 0
									? direction.map(
										onRight: onPoint1 ? pointEntity2 : Option<GameEntity>.None,
										onLeft: onPoint2 ? pointEntity1 : Option<GameEntity>.None
									)
									: direction.map(
										onUp: onPoint1 ? pointEntity2 : Option<GameEntity>.None,
										onDown: onPoint2 ? pointEntity1 : Option<GameEntity>.None
									)).IfSome(targetPoint => {
										player.ReplaceRewindPointIndex(player.previousPointIndex.value);
										player.ReplacePreviousPointIndex(player.pointIndex.value);
										player.ReplacePointIndex(targetPoint.pointIndex.value); 
									});
							})
						);
					}
				});
			}
		});
	}

	Option<MoveDirection> getMoveDirection() {
		var inputService = input.input.value;

		if (inputService.getMoveRightButton()) return MoveDirection.Right;
		if (inputService.getMoveLeftButton()) return MoveDirection.Left;
		if (inputService.getMoveUpButton()) return MoveDirection.Up;
		if (inputService.getMoveDownButton()) return MoveDirection.Down;

		return Option<MoveDirection>.None;
	}
}
