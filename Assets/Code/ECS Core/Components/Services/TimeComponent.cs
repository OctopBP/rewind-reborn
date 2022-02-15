using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.Services;

[Unique]
public class TimeComponent : IComponent, IService {
	public ITimeService value;
}