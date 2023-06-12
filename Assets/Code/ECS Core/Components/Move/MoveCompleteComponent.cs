using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public class MoveCompleteComponent : IComponent {
	public bool value;
}

public static class MoveCompleteComponentExt {
	public static bool isMoveComplete(this GameEntity entity) =>
		entity.maybeMoveComplete.Map(_ => _.value).IfNone(false);
}