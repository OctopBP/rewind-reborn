using Entitas;
using Rewind.Extensions;

[Game]
public class TraveledValueComponent : IComponent {
	public float value;
}

public static class TraveledValueComponentExt {
	/// <returns>value clamped between 0 and 1</returns>
	public static float clampedValue(this TraveledValueComponent tvc) => tvc.value.clamp01();
}
