using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Self)]
public class GearTypeCStateComponent : ValueComponent<GearTypeCState> { }