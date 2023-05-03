using System.Collections.Generic;
using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;

public class RecordMoveCompleteSystem : ReactiveSystem<GameEntity> {
	readonly GameContext game;

	public RecordMoveCompleteSystem(Contexts contexts) : base(contexts.game) {
		game = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.AnyOf(GameMatcher.MoveComplete));

	protected override bool Filter(GameEntity entity) => entity.isPlayer;

	protected override void Execute(List<GameEntity> players) {
		if (!game.clockEntity.clockState.value.isRecord()) return;

		foreach (var player in players) {
			game.createMoveCompleteTimePoint(player.isMoveComplete());
		}
	}
}