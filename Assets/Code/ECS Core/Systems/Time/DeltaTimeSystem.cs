using Entitas;
using Rewind.SharedData;

public class DeltaTimeSystem : IExecuteSystem
{
	private readonly GameContext game;
	private readonly IGroup<GameEntity> clocks;
	private readonly GameSettingsComponent gameSettingsComponent;

	private bool firstExecute;

	public DeltaTimeSystem(Contexts contexts)
	{
		game = contexts.game;
		gameSettingsComponent = contexts.config.gameSettings;
		clocks = game.GetGroup(GameMatcher.AllOf(GameMatcher.Clock, GameMatcher.ClockState));
		firstExecute = true;
	}

	public void Execute()
	{
		foreach (var clock in clocks.GetEntities())
		{
			// We need skip first Execute to sync clock.time with Time.time
			var delta = firstExecute ? 0 : game.worldTime.value.deltaTime;
			firstExecute = false;

			var config = gameSettingsComponent.value;
			var timeSpeed = clock.clockState.value.Fold(
				onRecord: config._clockNormalSpeed,
				onRewind: config._clockRewindSpeed,
				onReplay: config._clockNormalSpeed
			);
			
			clock.ReplaceDeltaTime(delta * timeSpeed);
		}
	}
}