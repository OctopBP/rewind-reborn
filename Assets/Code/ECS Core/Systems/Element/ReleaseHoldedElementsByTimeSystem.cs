using Entitas;

public class ReleaseHoldedElementsByTimeSystem : IExecuteSystem
{
	private readonly GameEntity clock;
	private readonly ConfigEntity settings;
	private readonly IGroup<GameEntity> elements;

	public ReleaseHoldedElementsByTimeSystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		settings = contexts.config.gameSettingsEntity;
		elements = contexts.game.GetGroup(GameMatcher.HoldedAtTime);
	}

	public void Execute()
	{
		var rrrCycleTime = settings.gameSettings.value._rewindTime * 3;
		
		foreach (var element in elements.GetEntities())
		{
			if (element.holdedAtTime.value + rrrCycleTime < clock.time.value)
			{
				element.RemoveHoldedAtTime();
			}
		}
	}
}