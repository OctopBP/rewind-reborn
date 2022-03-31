using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class PlatformAOpenPointSystem : IExecuteSystem {
	readonly IGroup<GameEntity> platforms;
	readonly IGroup<GameEntity> points;

	public PlatformAOpenPointSystem(Contexts contexts) {
		platforms = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.PlatformA, GameMatcher.PlatformAData, GameMatcher.PlatformAMoveTime, GameMatcher.PointIndex
		));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.PointOpenStatus
		));
	}

	public void Execute() {
		foreach (var platform in platforms.GetEntities()) {
			points.first(platform.isSamePoint).IfSome(point => {
				point.ReplacePointOpenStatus(platform.platformAMoveTime.value switch {
					var t when t <= platform.platformAData.value.openLimit => PointOpenStatus.ClosedRight,
					var t when t >= 1 - platform.platformAData.value.openLimit => PointOpenStatus.ClosedLeft,
					_ => PointOpenStatus.ClosedLeft | PointOpenStatus.ClosedRight
				});
			});
		}
	}
}