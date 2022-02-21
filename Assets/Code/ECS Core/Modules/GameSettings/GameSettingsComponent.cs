using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.Data;

[Game, Unique]
public class GameSettingsComponent : IComponent {
	public GameSettingsData value;
}