using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public class LevelProgressComponent : IComponent {
	public int value;
}