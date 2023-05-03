using System.Collections.Generic;
using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;

public class RecordButtonASystem : ReactiveSystem<GameEntity> {
	readonly GameContext game;
	readonly IGroup<GameEntity> buttons;

	public RecordButtonASystem(Contexts contexts) : base(contexts.game) {
		game = contexts.game;
		buttons = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.ButtonA, GameMatcher.ButtonAState, GameMatcher.Rotation
		));
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.AnyOf(GameMatcher.ClockState).AddedOrRemoved());

	protected override bool Filter(GameEntity entity) => true;

	protected override void Execute(List<GameEntity> _) {
		// Trigger when it becomes Rewind 
		if (!game.clockEntity.clockState.value.isRewind()) return;

		foreach (var button in buttons.GetEntities()) {
			game.createButtonATimePoint(button.id.value, button.buttonAState.value);
		}
	}
}