using ECSCore.Code.ECS_Core.Extensions;
using Entitas;
using Rewind.Services;

public class SetParentToCharacterSystem : IExecuteSystem {
	readonly IGroup<GameEntity> points;
	readonly IGroup<GameEntity> characters;

	public SetParentToCharacterSystem(Contexts contexts) {
		points = contexts.game.points();
		characters = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Character, GameMatcher.Position)
		);
	}

	public void Execute() {
		foreach (var character in characters.GetEntities()) {
			points.findPointOf(character).IfSome(point => {
				if (point.hasParentTransform) {
					character.ReplaceParentTransform(point.parentTransform.value);
				} else if (character.hasParentTransform) {
					character.RemoveParentTransform();
				}
			});
		}
	}
}