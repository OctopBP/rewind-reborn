using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Self)]
public class GearTypeAStateComponent : ValueComponent<GearTypeAState> { }