using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Input, Event(EventTarget.Self)]
public class TickComponent : IComponent {
	public int value;
}