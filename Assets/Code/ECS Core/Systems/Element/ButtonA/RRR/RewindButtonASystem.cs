using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class RewindButtonASystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> buttons;
	private readonly IGroup<GameEntity> timePoints;
	private readonly GameEntity clock;

	public RewindButtonASystem(Contexts contexts)
	{
		clock = contexts.game.clockEntity;
		buttons = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.ButtonA, GameMatcher.ButtonAState, GameMatcher.Id
		));
		timePoints = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Timestamp, GameMatcher.ButtonAState, GameMatcher.IdRef
		).NoneOf(
			GameMatcher.TimePointUsed
		));
	}

	public void Execute()
	{
		if (!clock.clockState.value.IsRewind()) return;

		foreach (var button in buttons.GetEntities())
		{
			var maybeTimePoint = timePoints.First(
				p => p.timestamp.value >= clock.time.value && p.idRef.value == button.id.value
			);

			maybeTimePoint.IfSome(timePoint =>
			{
				button.ReplaceButtonAState(timePoint.buttonAState.value.RewindState());
				timePoint.SetTimePointUsed(true);
			});
		}
	}
}