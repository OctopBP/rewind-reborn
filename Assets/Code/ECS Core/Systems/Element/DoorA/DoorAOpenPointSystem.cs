using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class DoorAOpenPointSystem : IExecuteSystem {
	readonly IGroup<GameEntity> doors;
	readonly IGroup<GameEntity> points;

	public DoorAOpenPointSystem(Contexts contexts) {
		doors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.DoorA, GameMatcher.DoorAState, GameMatcher.DoorAPoints
		));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint, GameMatcher.PointOpenStatus
		));
	}

	public void Execute() {
		foreach (var door in doors.GetEntities()) {
			var doorIsOpen = door.doorAState.value.isOpened();

			for (var i = 0; i < door.doorAPoints.value.Count; i++) {
				var pointIndex = door.doorAPoints.value[i];

				var state = i switch {
					_ when doorIsOpen => PointOpenStatus.Opened,
					0 => PointOpenStatus.ClosedRight,
					var x when x == door.doorAPoints.value.Count - 1 => PointOpenStatus.ClosedLeft,
					_ => ~PointOpenStatus.Opened
				};

				points.first(p => pointIndex.isSamePoint(p)).IfSome(point => point.ReplacePointOpenStatus(state));
			}
		}
	}
}