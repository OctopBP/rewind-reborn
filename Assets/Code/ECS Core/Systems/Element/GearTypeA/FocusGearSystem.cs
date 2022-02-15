using Entitas;
using Rewind.Services;

public class FocusGearSystem : IExecuteSystem {
	readonly IGroup<GameEntity> gears;
	readonly IGroup<GameEntity> characters;

	public FocusGearSystem(Contexts contexts) {
		gears = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Gear, GameMatcher.PathIndex, GameMatcher.PointIndex
		));

		characters = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Character, GameMatcher.PathIndex, GameMatcher.PointIndex,
			GameMatcher.PreviousPointIndex, GameMatcher.MoveComplete
		));
	}

	public void Execute() {
		foreach (var gear in gears.GetEntities()) {
			gear.isFocus = characters.any(gear.onPoint);
		}
	}
}