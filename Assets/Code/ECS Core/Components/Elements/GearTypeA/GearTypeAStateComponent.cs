using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.ECSCore.Enums;

[Game, Event(EventTarget.Self)]
public class GearTypeAStateComponent : IComponent {
	public GearTypeAState value;
}