using Entitas;
using Entitas.CodeGeneration.Attributes;
using Octop.ComponentModel.Attribute;
using Rewind.ECSCore.Enums;

[Game, Event(EventTarget.Self), ComponentModel]
public class ClockStateComponent : IComponent {
	public ClockState value;
}