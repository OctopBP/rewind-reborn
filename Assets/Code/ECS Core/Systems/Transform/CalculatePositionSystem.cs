using Entitas;
using LanguageExt;

public class CalculatePositionSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> entities;

	public CalculatePositionSystem(Contexts contexts)
	{
		entities = contexts.game.GetGroup(GameMatcher.Position);
	}

	public void Execute()
	{
		foreach (var entity in entities.GetEntities())
        {
			var maybeParent = entity.maybeParentEntity_value;
			var position = entity.position.value;

			// TODO: Rewrite with something like .Fold (aka .Aggregate)
			while (maybeParent.IsSome)
            {
				maybeParent.IfSome(parent =>
                {
					position += parent.position.value;
					maybeParent = parent.maybeParentEntity_value;
				});
			}

			entity.ReplacePosition(position);
		}
	}
}