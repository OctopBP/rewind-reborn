using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Game, Event(EventTarget.Self)]
public class ConnectorStateComponent : IComponent {
	public ConnectorState value;
}