using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.ECSCore.Enums;

[Game, Event(EventTarget.Self)]
public class CharacterLookDirectionComponent : IComponent {
	public CharacterLookDirection value;
}