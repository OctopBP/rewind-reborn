using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.Services;

[Unique]
public class WorldTimeComponent : IComponent, IService {
	public ITimeService value;
}