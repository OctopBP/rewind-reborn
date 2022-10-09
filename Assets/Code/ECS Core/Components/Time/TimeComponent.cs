using Entitas;
using Entitas.CodeGeneration.Attributes;
using Octop.ComponentModel.Attribute;

[Game, Input, Event(EventTarget.Self), ComponentModel]
public class TimeComponent : IComponent {
	public float value;
}