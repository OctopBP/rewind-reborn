using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self), Event(EventTarget.Self, EventType.Removed, 1)]
public class HoldedAtTimeComponent : ValueComponent<float> { }