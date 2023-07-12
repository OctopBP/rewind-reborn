using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public class MoveCompleteComponent : ValueComponent<bool> { }

public static class MoveCompleteComponentExt {
	public static bool isMoveComplete(this GameEntity entity) =>
		entity.maybeMoveComplete.Match(Some: _ => _.value, None: false);
}