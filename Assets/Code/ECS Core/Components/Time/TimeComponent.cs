using Entitas.CodeGeneration.Attributes;

[Game, Input, Event(EventTarget.Self)]
public class TimeComponent : ValueComponent<float> { }