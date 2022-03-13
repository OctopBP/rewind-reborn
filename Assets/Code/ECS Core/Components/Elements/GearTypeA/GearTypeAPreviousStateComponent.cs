using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.ECSCore.Enums;

[Game]
public class GearTypeAPreviousStateComponent : IComponent {
	public GearTypeAState value;
}