using Entitas;
using Rewind.Services;

public class SetParentToCharacterSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> points;
	private readonly IGroup<GameEntity> characters;

	public SetParentToCharacterSystem(Contexts contexts)
	{
		points = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint)
		);
		characters = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Character, GameMatcher.Position)
		);
	}

	public void Execute()
	{
		foreach (var character in characters.GetEntities())
		{
			points.FindPointOf(character).IfSome(point =>
			{
				if (point.hasParentTransform)
				{
					character.ReplaceParentTransform(point.parentTransform.value);
				}
				else if (character.hasParentTransform)
				{
					character.RemoveParentTransform();
				}
			});
		}
	}
}