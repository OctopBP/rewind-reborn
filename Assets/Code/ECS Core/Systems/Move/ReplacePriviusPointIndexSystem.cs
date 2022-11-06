using System.Collections.Generic;
using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.ECSCore.Helpers;
using Rewind.Extensions;
using UnityEngine;

public class ReplacePreviousPointSystem : ReactiveSystem<GameEntity> {
	readonly GameContext game;

	public ReplacePreviousPointSystem(Contexts contexts) : base(contexts.game) {
		game = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.TargetReached);

	protected override bool Filter(GameEntity entity) => entity.hasPreviousPoint && entity.hasCurrentPoint;

	protected override void Execute(List<GameEntity> entities) {
		entities.ForEach(replacePoints);

		void replacePoints(GameEntity entity) {
			var currentPoint = entity.currentPoint.value;
			var rewindPoint = entity.previousPoint.value;

			if (game.clockEntity.clockState.value.isRecord()) {
				game.createMoveTimePoint(
					currentPoint: currentPoint, previousPoint: currentPoint,
					rewindPoint: rewindPoint
				);
			}

			entity.with(e => e.RemovePreviousPoint());
		}
	}
}