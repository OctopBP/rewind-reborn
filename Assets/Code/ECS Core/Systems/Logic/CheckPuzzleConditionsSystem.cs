using Entitas;
using Rewind.Extensions;

public class CheckPuzzleConditionsSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> puzzleGroups;
	private readonly GameContext game;

	public CheckPuzzleConditionsSystem(Contexts contexts)
	{
		game = contexts.game;
		puzzleGroups = game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Id, GameMatcher.ConditionGroup, GameMatcher.PuzzleValueReceiver)
		);
	}

	public void Execute()
	{
		var allEntities = game.GetEntities();

		// Check all puzzles
		foreach (var puzzleGroup in puzzleGroups.GetEntities())
        {
			var value = puzzleGroup.conditionGroup.value.CalculateValue(allEntities);
			
			// Check all receivers
			foreach (var puzzleValueReceiverItem in puzzleGroup.puzzleValueReceiver.value)
            {
				var filter = puzzleValueReceiverItem.EntityFilter();

				// Take and use receiverEntity
				foreach (var receiverEntity in allEntities.First(filter))
                {
					puzzleValueReceiverItem.ReceiveValue(receiverEntity, value);
				}
			}
		}
	}
}