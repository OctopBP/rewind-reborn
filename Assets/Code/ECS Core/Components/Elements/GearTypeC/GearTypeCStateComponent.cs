using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Self)]
public class GearTypeCStateComponent : IComponent {
	public GearTypeCState value;
}