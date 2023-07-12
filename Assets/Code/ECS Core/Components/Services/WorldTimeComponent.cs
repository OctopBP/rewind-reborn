using Entitas.CodeGeneration.Attributes;
using Rewind.Services;

[Unique]
public class WorldTimeComponent : ValueComponent<ITimeService>, IService { }