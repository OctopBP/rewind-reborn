using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class DoorAOpenPointSystem : IExecuteSystem {
	readonly IGroup<GameEntity> doors;
	readonly IGroup<GameEntity> points;

	public DoorAOpenPointSystem(Contexts contexts) {
		doors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.DoorA, GameMatcher.DoorAState, GameMatcher.Id, GameMatcher.CurrentPoint
		));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint
		));
	}

	public void Execute() {
		foreach (var door in doors.GetEntities()) {
			points.findPointOf(door).IfSome(point => {
				var doorIsOpen = door.doorAState.value.isOpened();
				var doorId = door.id.value;
				
				if (doorIsOpen) {
					point.leftPathDirectionBlocks.removeAllByGuid(doorId);
				} else {
					point.leftPathDirectionBlocks.replaceWithBlockBoth(doorId);
				}
			});
		}
	}
}