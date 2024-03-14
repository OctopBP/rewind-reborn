using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;
using static Rewind.SharedData.ButtonAState;
using static LanguageExt.Prelude;

public class ButtonAStateSystem : IExecuteSystem
{
	private readonly GameContext game;
	private readonly IGroup<GameEntity> buttons;

	public ButtonAStateSystem(Contexts contexts)
	{
		game = contexts.game;
		buttons = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.ButtonA, GameMatcher.ButtonAState
		));
	}

	public void Execute()
	{
		var clockState = game.clockEntity.clockState.value;
		if (clockState.IsRewind()) return;

		foreach (var button in buttons.GetEntities())
		{
			if (clockState.IsReplay() && button.hasHoldedAtTime) continue;

			var currentState = button.buttonAState.value;
			(currentState switch
			{
				Closed => button.isActive ? Some(Opened) : None,
				Opened => button.isActive ? None : Some(Closed),
				_ => None
			}).IfSome(newState =>
			{
				game.CreateButtonATimePoint(button.id.value, newState);
				button.ReplaceButtonAState(newState);
				if (clockState.IsRecord())
				{
					button.ReplaceHoldedAtTime(game.clockEntity.time.value);
				}
			});
		}
	}
}