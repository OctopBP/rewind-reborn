using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game]
public class GearTypeAPreviousStateComponent : IComponent {
	public GearTypeAState value;
}