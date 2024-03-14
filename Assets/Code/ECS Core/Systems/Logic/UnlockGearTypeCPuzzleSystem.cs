using System.Collections.Generic;
using Entitas;

public class LockGearTypeCPuzzleSystem : ReactiveSystem<GameEntity>
{
	private readonly IGroup<GameEntity> elements;

	public LockGearTypeCPuzzleSystem(Contexts contexts) : base(contexts.game)
    {
		elements = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Id, GameMatcher.PuzzleElement
		));
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.AnyOf(GameMatcher.PuzzleComplete));

	protected override bool Filter(GameEntity entity) => entity.isPuzzleGroup && entity.isPuzzleComplete;

	protected override void Execute(List<GameEntity> _)
    {
		foreach (var element in elements.GetEntities())
        {
			element.SetGearTypeCLocked(true);
		}
	}
}