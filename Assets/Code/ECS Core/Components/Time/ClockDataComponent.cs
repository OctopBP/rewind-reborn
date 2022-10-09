using Entitas;
using Octop.ComponentModel.Attribute;
using Rewind.Data;

[Game, ComponentModel]
public class ClockDataComponent : IComponent {
	public ClockData value;
}