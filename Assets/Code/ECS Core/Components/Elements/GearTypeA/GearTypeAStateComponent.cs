using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Self)]
public class GearTypeAStateComponent : IComponent {
	public GearTypeAState value;
}