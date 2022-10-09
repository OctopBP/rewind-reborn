using Entitas;
using Octop.ComponentModel.Attribute;

[Game, ComponentModel]
public class PointIndexComponent : IComponent {
	public PathPointType value;
}
