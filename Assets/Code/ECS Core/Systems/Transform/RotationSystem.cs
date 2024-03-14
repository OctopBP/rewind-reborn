using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RotationSystem : ReactiveSystem<GameEntity>
{
	public RotationSystem(Contexts contexts) : base(contexts.game) { }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
		context.CreateCollector(GameMatcher.Rotation);

	protected override bool Filter(GameEntity entity) => entity.hasRotation && entity.hasView;

	protected override void Execute(List<GameEntity> entities)
    {
		foreach (var entity in entities)
        {
			var localEulerAngles = entity.view.value.transform.localEulerAngles;
			var newRotation = new Vector3(localEulerAngles.x, localEulerAngles.y, entity.rotation.value);
			entity.view.value.transform.localEulerAngles = newRotation;
		}
	}
}