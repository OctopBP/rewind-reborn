using Entitas;
using LanguageExt;
using Rewind.Extensions;
using Rewind.SharedData;
using Rewind.Services;
using static LanguageExt.Prelude;

public class CommandLadderMoveSystem : IExecuteSystem {
	readonly InputContext input;
	readonly IGroup<GameEntity> players;
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> ladderPoints;
	readonly IGroup<GameEntity> ladderConnectors;
	readonly GameEntity clock;

	public CommandLadderMoveSystem(Contexts contexts) {
		input = contexts.input;
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CurrentPoint));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.Depth
		));
		ladderPoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.LadderStair, GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.Depth
		));
		ladderConnectors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.LadderStair, GameMatcher.LadderConnector, GameMatcher.Point,
			GameMatcher.CurrentPoint, GameMatcher.Depth
		));
	}

	public void Execute() {
		if (clock.clockState.value.isRewind()) return;

		input.input.getMoveDirection().IfSome(direction => {
			foreach (var player in players.GetEntities()) {
				points.findPointOf(player).IfSome(playerPoint => {
					if (playerPoint.isLadderStair) {
						direction.foldByAxis(
							onHorizontal: _ => {
								if (!playerPoint.hasLadderConnector) return;
								
								var connectorPoint = playerPoint.ladderConnector.value;
								player
									.ReplacePreviousPoint(player.currentPoint.value)
									.ReplaceCurrentPoint(connectorPoint);
							},
							onVertical: vertical => {
								var currentPoint = player.currentPoint.value;
								var maybePreviousPoint = player.maybePreviousPoint.Map(_ => _.value);
								maybeNextPoint(currentPoint, maybePreviousPoint, vertical)
									.Match(
										nextPoint => {
											player
												.ReplacePreviousPoint(currentPoint)
												.ReplaceCurrentPoint(nextPoint);
											
											if (player.isMoveComplete()) {
												player.ReplaceMoveComplete(false);
											}
									  }, 
										() => {
											if (player.isTargetReached != player.isMoveComplete()) {
												player.ReplaceMoveComplete(player.isTargetReached);
											}
										}
								  );
							}
						);
					}
					else if (direction.isUp()) {
						ladderConnectors
							.first(p => playerPoint.isSamePoint(p.ladderConnector.value))
							.IfSome(connectorPoint => {
								player
									.ReplacePreviousPoint(player.currentPoint.value)
									.ReplaceCurrentPoint(connectorPoint.currentPoint.value);
							});
					}
				});
			}
		});
	}
	
	Option<PathPoint> maybeNextPoint(
		PathPoint currentPoint, Option<PathPoint> maybePreviousPoint, VerticalMoveDirection direction
	) {
		var nextPoint = currentPoint.pathWithIndex(currentPoint.index + direction.intValue());
		return nextPointExist() && canReach() && isSamePath()
			? Some(nextPoint)
			: None;
		
		bool nextPointExist() => ladderPoints.any(p => p.isSamePoint(nextPoint));
		bool canReach() => maybePreviousPoint.Map(pp => (nextPoint.index - pp.index).abs() < 2).IfNone(true);
		bool isSamePath() => maybePreviousPoint.Map(pp => currentPoint.pathId == pp.pathId).IfNone(true);
	}
}
