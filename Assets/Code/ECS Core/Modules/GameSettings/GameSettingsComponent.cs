using Entitas;
using Entitas.CodeGeneration.Attributes;
using Rewind.SharedData;

[Config, Unique]
public class GameSettingsComponent : IComponent
{
	public GameSettingsData value;
}