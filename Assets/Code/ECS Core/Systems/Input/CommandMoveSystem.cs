using System.Linq;
using Entitas;
using LanguageExt;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;
using Rewind.Extensions;
using Rewind.Services;
using static LanguageExt.Prelude;

public class CommandMoveSystem : IExecuteSystem
{
	private readonly InputContext input;
	private readonly GameContext game;
	private readonly IGroup<GameEntity> players;
	private readonly IGroup<GameEntity> points;

	public CommandMoveSystem(Contexts contexts)
	{
		input = contexts.input;
		game = contexts.game;
		players = game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CurrentPoint));
		points = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.LeftPathDirectionBlocks
		));
	}

	public void Execute()
	{
		var clockState = game.clockEntity.clockState.value;
		if (clockState.IsRewind()) return;

		var maybeDirection = input.input.GetMoveDirection().FlatMap(direction => direction.AsHorizontal());
		foreach (var player in players.GetEntities())
        {
			var currentPoint = player.currentPoint.value;
			var maybePreviousPoint = player.maybePreviousPoint_value;

			maybeDirection
				.FlatMap(direction => MaybeNextPoint(currentPoint, maybePreviousPoint, direction))
				.Match(
					Some: nextPoint =>
                    {
						if (clockState.IsRecord())
                        {
							game.CreateMoveTimePoint(
								currentPoint: nextPoint, previousPoint: currentPoint,
								rewindPoint: maybePreviousPoint.IfNone(currentPoint)
							);
						}

						if (maybePreviousPoint.Map(previousPoint => previousPoint == nextPoint).IfNone(false))
                        {
							player.ReplaceTraveledValue(1 - player.traveledValue.clampedValue());
						}

						player
							.ReplaceCurrentPoint(nextPoint)
							.ReplacePreviousPoint(currentPoint);
						// .ReplaceMoveComplete(false);

						if (player.IsMoveComplete())
                        {
							player.ReplaceMoveComplete(false);
						}
					},
					None: () =>
                    {
						if (player.isTargetReached != player.IsMoveComplete())
                        {
							player.ReplaceMoveComplete(player.isTargetReached);
						}
					}
				);
		}
	}

	private Option<PathPoint> MaybeNextPoint(
		PathPoint currentPoint, Option<PathPoint> maybePreviousPoint, HorizontalMoveDirection direction
	) {
		var nextPoint = currentPoint.PathWithIndex(currentPoint.index + direction.INTValue());
		var blockerPoint = direction.IsRight() ? nextPoint : currentPoint;
		
		return NextPointExist() && CanReach() && IsSamePath() && CanMoveOnLeftFromPoint()
			? Some(nextPoint)
			: None;
		
		bool CanReach() => maybePreviousPoint.Map(pp => (nextPoint.index - pp.index).Abs() < 2).IfNone(true);

		bool IsSamePath() => maybePreviousPoint.Map(pp => currentPoint.pathId == pp.pathId).IfNone(true);

		bool NextPointExist() => points.Any(p => p.IsSamePoint(nextPoint));
		
		bool CanMoveOnLeftFromPoint() => points
			.First(p => p.IsSamePoint(blockerPoint))
			.Map(p => !p.leftPathDirectionBlocks.value.Any(b => direction.BlockedBy(b._leftPathDirectionBlock)))
			.IfNone(false);
	}
}
