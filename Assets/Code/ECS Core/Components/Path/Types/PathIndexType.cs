using System;

[Serializable]
public record PathIndexType(int value) {
	public int value { get; } = value;

	public static implicit operator int(PathIndexType index) => index.value;
	public static implicit operator PathIndexType(int index) => new(index);
}