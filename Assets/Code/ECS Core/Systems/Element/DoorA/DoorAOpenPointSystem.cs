using Entitas;
using Rewind.ECSCore.Enums;
using Rewind.Services;

public class DoorAOpenPointSystem : IExecuteSystem {
	readonly IGroup<GameEntity> doors;
	readonly IGroup<GameEntity> points;

	public DoorAOpenPointSystem(Contexts contexts) {
		doors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.DoorA, GameMatcher.DoorAState, GameMatcher.PointIndex
		));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.PointIndex, GameMatcher.PointOpenStatus
		));
	}

	public void Execute() {
		foreach (var door in doors.GetEntities()) {
			points.first(door.isSamePoint).IfSome(point => {
				point.ReplacePointOpenStatus(door.doorAState.value.isOpened()
					? PointOpenStatus.Opened
					: ~PointOpenStatus.Opened
				);
			});
		}
	}
}