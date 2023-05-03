using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Self)]
public class PlatformAStateComponent : IComponent {
	public PlatformAState value;
}