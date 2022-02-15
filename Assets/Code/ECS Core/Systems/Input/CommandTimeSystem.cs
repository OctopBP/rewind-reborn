using Entitas;

public class CommandTimeSystem : IExecuteSystem {
	readonly GameContext game;
	readonly InputContext input;
	readonly IGroup<GameEntity> clocks;

	public CommandTimeSystem(Contexts contexts) {
		game = contexts.game;
		input = contexts.input;

		// clocks = contexts.game
		// 	.GetGroup(GameMatcher
		// 		.AllOf(GameMatcher.Clock)
		// 		.NoneOf(GameMatcher.Rewind, GameMatcher.Replay));
	}

	public void Execute() {
		// InputEntity rewindTime = input.rewindTimeEntity;
		// if (!rewindTime.isKeyDown) return;

		foreach (var clock in clocks.GetEntities()) {
			// clock.isRewind = true;
			// game.logger.Value.LogMessage($"[{game.clockEntity.timeTick.Value}] -> Rewind");
			// clock.ReplaceTimer(game.settings.Value.RewindTime);
		}
	}
}