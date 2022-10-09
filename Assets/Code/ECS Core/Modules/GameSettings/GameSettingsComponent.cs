using Entitas;
using Entitas.CodeGeneration.Attributes;
using Octop.ComponentModel.Attribute;
using Rewind.Data;

[Game, Unique, ComponentModel]
public class GameSettingsComponent : IComponent {
	public GameSettingsData value;
}