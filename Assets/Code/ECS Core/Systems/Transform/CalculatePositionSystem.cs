using Entitas;
using LanguageExt;

public class CalculatePositionSystem : IExecuteSystem {
	readonly IGroup<GameEntity> entities;

	public CalculatePositionSystem(Contexts contexts) {
		entities = contexts.game.GetGroup(GameMatcher.Position);
	}

	public void Execute() {
		foreach (var entity in entities.GetEntities()) {
			var maybeParent = getMaybeParent(entity);
			var position = entity.position.value;

			// TODO: Rewrite with something like .Fold (aka .Aggregate)
			while (maybeParent.IsSome) {
				maybeParent.IfSome(parent => {
					position += parent.position.value;
					maybeParent = getMaybeParent(parent);
				});
			}

			entity.ReplacePosition(position);

			Option<GameEntity> getMaybeParent(GameEntity e) => e.maybeParentEntity.Map(_ => _.value);
		}
	}
}