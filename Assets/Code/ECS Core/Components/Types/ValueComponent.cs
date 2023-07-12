using Entitas;
using Entitas.CodeGeneration.Attributes;

[DontGenerate]
public abstract class ValueComponent<T> : IComponent {
	public T value;
}