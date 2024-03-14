using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;

public class LeverAStateSystem : IExecuteSystem
{
	private readonly GameContext game;
	private readonly IGroup<GameEntity> levers;
	private readonly InputContext input;

	public LeverAStateSystem(Contexts contexts)
	{
		game = contexts.game;
		input = contexts.input;
		levers = game.GetGroup(GameMatcher
			.AllOf(GameMatcher.LeverA, GameMatcher.LeverAState)
		);
	}

	public void Execute()
	{
		var clockState = game.clockEntity.clockState.value;
		if (clockState.IsRewind()) return;
		if (!input.input.value.GetInteractButtonDown()) return;
		
		foreach (var lever in levers.GetEntities())
		{
			if ((clockState.IsReplay() && lever.hasHoldedAtTime) || !lever.isActive) continue;

			var newState = lever.leverAState.value.RewindState();
			game.CreateLeverATimePoint(lever.id.value, newState);
			lever.ReplaceLeverAState(newState);
			if (clockState.IsRecord())
			{
				lever.ReplaceHoldedAtTime(game.clockEntity.time.value);
			}
		}
	}
}