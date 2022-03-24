using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.ECSCore.Enums;

[Game, Event(EventTarget.Self)]
public class ButtonAStateComponent : IComponent {
	public ButtonAState value;
}