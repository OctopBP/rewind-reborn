using Entitas;
using Rewind.SharedData;
using static LanguageExt.Prelude;

public class CharacterLookDirectionSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> players;
	private readonly GameEntity clock;

	public CharacterLookDirectionSystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		players = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Character, GameMatcher.CurrentPoint, GameMatcher.PreviousPoint,
			GameMatcher.CharacterLookDirection
		));
	}

	public void Execute()
	{
		var isRewind = clock.clockState.value.IsRewind();
		foreach (var player in players.GetEntities())
		{
			var previousPoint = player.previousPoint.value;
			var samePath = player.currentPoint.value.pathId == previousPoint.pathId;
			var indexDiff = samePath ? player.currentPoint.value.index - previousPoint.index : 0;
			var maybeNewDirection = indexDiff switch
			{
				< 0 => Some(isRewind ? CharacterLookDirection.Right : CharacterLookDirection.Left),
				> 0 => Some(isRewind ? CharacterLookDirection.Left : CharacterLookDirection.Right),
				_ => None,
			};

			var currentDirection = player.characterLookDirection.value;
			maybeNewDirection
				.Filter(newDirection => newDirection != currentDirection)
				.IfSome(newDirection => player.ReplaceCharacterLookDirection(newDirection));
		}
	}
}