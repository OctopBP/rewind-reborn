using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PositionSystem : ReactiveSystem<GameEntity> {
	public PositionSystem(Contexts contexts) : base(contexts.game) { }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.Position);

	protected override bool Filter(GameEntity entity) => entity.hasPosition && entity.hasView;

	protected override void Execute(List<GameEntity> entities) {
		foreach (var entity in entities) {
			var newPosition = new Vector3(
				entity.position.value.x, entity.position.value.y, entity.view.value.transform.position.z
			);
			entity.view.value.transform.position = newPosition;
		}
	}
}