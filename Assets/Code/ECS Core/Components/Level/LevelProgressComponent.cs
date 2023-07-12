using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public class LevelProgressComponent : ValueComponent<int> { }