using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.ECSCore.Enums;

[Game, Event(EventTarget.Any)]
public class ClockStateComponent : IComponent {
	public ClockState value;
}