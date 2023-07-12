using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Any)]
public class ClockStateComponent : ValueComponent<ClockState> { }