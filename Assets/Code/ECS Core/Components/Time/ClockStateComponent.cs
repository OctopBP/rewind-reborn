using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self, EventType.Added, 0), Event(EventTarget.Self, EventType.Removed, 1)]
public class ReplayComponent : IComponent { }