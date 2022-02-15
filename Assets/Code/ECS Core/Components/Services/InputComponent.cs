using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.Services;

[Input, Unique]
public class InputComponent : IComponent, IService {
	public IInputService value;
}