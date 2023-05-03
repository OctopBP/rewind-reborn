using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Self)]
public class DoorAStateComponent : IComponent {
	public DoorAState value;
}