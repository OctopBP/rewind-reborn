using Entitas;

public class PendulumOpenPointSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> pendulums;
	private readonly IGroup<GameEntity> points;

	public PendulumOpenPointSystem(Contexts contexts)
	{
		pendulums = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Pendulum, GameMatcher.PendulumData, GameMatcher.Rotation, GameMatcher.CurrentPoint)
		);
		points = contexts.game.GetGroup(GameMatcher
			.AllOf(GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.LeftPathDirectionBlocks)
		);
	}

	public void Execute()
	{
		foreach (var pendulum in pendulums.GetEntities())
		{
			// points.first(pendulum.isSamePoint).IfSome(point =>
			// 	point.ReplacePointOpenStatus(pendulum.rotation.value switch {
			// 		var r when r > pendulum.pendulumData.value._openLimit => PointLeftConnectorMoveStatus.ClosedLeft,
			// 		var r when r < -pendulum.pendulumData.value._openLimit => PointLeftConnectorMoveStatus.ClosedRight,
			// 		_ => ~PointLeftConnectorMoveStatus.Opened
			// }));
		}
	}
}