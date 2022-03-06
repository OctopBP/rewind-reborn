using System;

[Serializable]
public record PointIndexType(int value) {
	public int value { get; } = value;

	public static implicit operator int(PointIndexType index) => index.value;
	public static implicit operator PointIndexType(int index) => new(index);
}