using Entitas;
using LanguageExt;
using Rewind.Extensions;
using Rewind.SharedData;
using Rewind.Services;
using static LanguageExt.Prelude;

public class CommandLadderMoveSystem : IExecuteSystem
{
	private readonly InputContext input;
	private readonly IGroup<GameEntity> players;
	private readonly IGroup<GameEntity> points;
	private readonly IGroup<GameEntity> ladderPoints;
	private readonly IGroup<GameEntity> ladderConnectors;
	private readonly GameEntity clock;

	public CommandLadderMoveSystem(Contexts contexts)
	{
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

	public void Execute()
	{
		if (clock.clockState.value.IsRewind()) return;

		input.input.GetMoveDirection().IfSome(direction =>
        {
			foreach (var player in players.GetEntities())
            {
				points.FindPointOf(player).IfSome(playerPoint =>
                {
					if (playerPoint.isLadderStair)
                    {
						direction.FoldByAxis(
							onHorizontal: _ =>
                            {
								if (!playerPoint.hasLadderConnector) return;
								
								var connectorPoint = playerPoint.ladderConnector.value;
								player
									.ReplacePreviousPoint(player.currentPoint.value)
									.ReplaceCurrentPoint(connectorPoint);
							},
							onVertical: vertical =>
                            {
								var currentPoint = player.currentPoint.value;
								var maybePreviousPoint = player.maybePreviousPoint_value;
								MaybeNextPoint(currentPoint, maybePreviousPoint, vertical)
									.Match(
										nextPoint =>
                                        {
											player
												.ReplacePreviousPoint(currentPoint)
												.ReplaceCurrentPoint(nextPoint);
											
											if (player.IsMoveComplete())
                                            {
												player.ReplaceMoveComplete(false);
											}
										}, 
										() =>
                                        {
											if (player.isTargetReached != player.IsMoveComplete())
                                            {
												player.ReplaceMoveComplete(player.isTargetReached);
											}
										}
								  );
							}
						);
					}
					else if (direction.IsUp())
                    {
						ladderConnectors
							.First(p => playerPoint.IsSamePoint(p.ladderConnector.value))
							.IfSome(connectorPoint =>
                            {
								player
									.ReplacePreviousPoint(player.currentPoint.value)
									.ReplaceCurrentPoint(connectorPoint.currentPoint.value);
							});
					}
				});
			}
		});
	}

	private Option<PathPoint> MaybeNextPoint(
		PathPoint currentPoint, Option<PathPoint> maybePreviousPoint, VerticalMoveDirection direction
	) {
		var nextPoint = currentPoint.PathWithIndex(currentPoint.index + direction.INTValue());
		return NextPointExist() && CanReach() && IsSamePath()
			? Some(nextPoint)
			: None;
		
		bool NextPointExist() => ladderPoints.Any(p => p.IsSamePoint(nextPoint));
		bool CanReach() => maybePreviousPoint.Map(pp => (nextPoint.index - pp.index).Abs() < 2).IfNone(true);
		bool IsSamePath() => maybePreviousPoint.Map(pp => currentPoint.pathId == pp.pathId).IfNone(true);
	}
}
