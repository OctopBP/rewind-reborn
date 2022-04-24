using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.ECSCore.Enums;

[Game]
public class GearTypeCPreviousStateComponent : IComponent {
	public GearTypeCState value;
}