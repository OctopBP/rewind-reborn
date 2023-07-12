using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public class DepthComponent : ValueComponent<int> { }

public static class DepthComponentExt {
	public static int distance(this DepthComponent @this, DepthComponent other) => @this.value - other.value;
	public static bool equal(this DepthComponent @this, DepthComponent other) => @this.value == other.value;
}