using Entitas;
using Rewind.SharedData;
using Rewind.Services;

public class DoorAOpenPointSystem : IExecuteSystem
{
	private readonly IGroup<GameEntity> doors;
	private readonly IGroup<GameEntity> points;

	public DoorAOpenPointSystem(Contexts contexts)
	{
		doors = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.DoorA, GameMatcher.DoorAState, GameMatcher.Id, GameMatcher.CurrentPoint
		));
		points = contexts.game.GetGroup(GameMatcher.AllOf(
			GameMatcher.Point, GameMatcher.CurrentPoint
		));
	}

	public void Execute()
	{
		foreach (var door in doors.GetEntities())
		{
			points.FindPointOf(door).IfSome(point =>
			{
				var doorIsOpen = door.doorAState.value.IsOpened();
				var doorId = door.id.value;
				
				if (doorIsOpen)
				{
					point.leftPathDirectionBlocks.RemoveAllByGuid(doorId);
				}
				else
				{
					point.leftPathDirectionBlocks.ReplaceWithBlockBoth(doorId);
				}
			});
		}
	}
}