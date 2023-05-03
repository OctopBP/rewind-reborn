using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Any)]
public class ClockStateComponent : IComponent {
	public ClockState value;
}