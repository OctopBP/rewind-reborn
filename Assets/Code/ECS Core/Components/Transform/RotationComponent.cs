using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public class RotationComponent : ValueComponent<float> { }