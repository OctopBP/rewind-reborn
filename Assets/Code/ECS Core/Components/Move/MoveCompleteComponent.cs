using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.Services;

[Game, Event(EventTarget.Self)]
public class MoveCompleteComponent : IComponent {
	public bool value;
}

public static class MoveCompleteComponentExt {
	public static bool isMoveComplete(this GameEntity entity) =>
		entity.maybeValue(_ => _.hasMoveComplete, _ => _.moveComplete.value).IfNone(false);
}