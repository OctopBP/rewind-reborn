using System.Collections.Generic;
using Entitas;
using Rewind.SharedData;
using Rewind.ECSCore.Helpers;

public class RecordMoveSystem : ReactiveSystem<GameEntity> {
	readonly GameContext game;
	readonly IGroup<GameEntity> players;

	public RecordMoveSystem(Contexts contexts) : base(contexts.game) {
		game = contexts.game;
		players = game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Player, GameMatcher.CurrentPoint
		));
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.AnyOf(GameMatcher.ClockState).AddedOrRemoved());

	protected override bool Filter(GameEntity entity) => true;

	protected override void Execute(List<GameEntity> _) {
		// Trigger when it becomes Rewind 
		if (!game.clockEntity.clockState.value.isRewind()) return;

		foreach (var player in players.GetEntities()) {
			var currentPoint = player.currentPoint.value;
			var maybePreviousPoint = player.maybePreviousPoint_value;
			game.createMoveTimePoint(
				currentPoint: maybePreviousPoint.IfNone(currentPoint), previousPoint: currentPoint,
				rewindPoint: maybePreviousPoint.IfNone(currentPoint)
			);
		}
	}
}