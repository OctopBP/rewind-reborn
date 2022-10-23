using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.Data;

[Config, Unique]
public class GameSettingsComponent : IComponent {
	public GameSettingsData value;
}