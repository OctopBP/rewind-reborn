using Entitas;
using UnityEngine;

public class ParentTransformSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> transforms;

	public ParentTransformSystem(Contexts contexts)
	{
		transforms = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.ParentTransform, GameMatcher.Position, GameMatcher.LocalPosition)
		);
	}

	public void Execute()
	{
		foreach (var transform in transforms.GetEntities())
		{
			var parentTransform = transform.parentTransform.value;
			var matrix = Matrix4x4.TRS(parentTransform.position, parentTransform.rotation, parentTransform.localScale);

			transform.ReplacePosition(matrix.MultiplyPoint(transform.localPosition.value));
		}
	}
}