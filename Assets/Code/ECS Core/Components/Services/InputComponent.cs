using Entitas.CodeGeneration.Attributes;
using LanguageExt;
using Rewind.SharedData;
using Rewind.Services;
using static LanguageExt.Prelude;

[Input, Unique]
public class InputComponent : ValueComponent<IInputService>, IService { }

public static class InputComponentExt
{
	public static Option<MoveDirection> GetMoveDirection(this InputComponent inputComponent)
	{
		var inputService = inputComponent.value;

		if (inputService.GetMoveRightButton())
        {
            return Some(MoveDirection.Right);
        }
		
		if (inputService.GetMoveLeftButton())
        {
            return Some(MoveDirection.Left);
        }
		
		if (inputService.GetMoveUpButton())
        {
            return Some(MoveDirection.Up);
        }
		
		if (inputService.GetMoveDownButton())
        {
            return Some(MoveDirection.Down);
        }

		return None;
	}
}