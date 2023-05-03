using Entitas;
using Entitas.CodeGeneration.Attributes;
using LanguageExt;
using Rewind.SharedData;
using Rewind.Services;
using static LanguageExt.Prelude;

[Input, Unique]
public class InputComponent : IComponent, IService {
	public IInputService value;
}

public static class InputComponentExt {
	public static Option<MoveDirection> getMoveDirection(this InputComponent inputComponent) {
		var inputService = inputComponent.value;

		if (inputService.getMoveRightButton()) return Some(MoveDirection.Right);
		if (inputService.getMoveLeftButton()) return Some(MoveDirection.Left);
		if (inputService.getMoveUpButton()) return Some(MoveDirection.Up);
		if (inputService.getMoveDownButton()) return Some(MoveDirection.Down);

		return None;
	}
}