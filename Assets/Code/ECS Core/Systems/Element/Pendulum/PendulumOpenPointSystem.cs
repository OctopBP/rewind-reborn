using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class PendulumOpenPointSystem : IExecuteSystem {
	readonly IGroup<GameEntity> pendulums;
	readonly IGroup<GameEntity> points;

	public PendulumOpenPointSystem(Contexts contexts) {
		pendulums = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Pendulum, GameMatcher.PendulumData, GameMatcher.Rotation, GameMatcher.PointIndex
		));

		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.PointOpenStatus
		));
	}

	public void Execute() {
		foreach (var pendulum in pendulums.GetEntities()) {
			points.first(pendulum.isSamePoint).IfSome(point => {
				point.ReplacePointOpenStatus(pendulum.rotation.value switch {
					var r when r > pendulum.pendulumData.value.openLimit => PointOpenStatus.ClosedLeft,
					var r when r < -pendulum.pendulumData.value.openLimit => PointOpenStatus.ClosedRight,
					_ => PointOpenStatus.ClosedLeft | PointOpenStatus.ClosedRight
				});
			});
		}
	}
}