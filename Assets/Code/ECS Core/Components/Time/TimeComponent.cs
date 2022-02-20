using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Input, Event(EventTarget.Self)]
public class TimeComponent : IComponent {
	public float value;
}