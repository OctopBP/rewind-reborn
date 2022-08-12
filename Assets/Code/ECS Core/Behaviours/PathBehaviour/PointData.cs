using System;
using Rewind.ECSCore.Enums;
using UnityEngine;

[Serializable]
public class PointData {
	public Vector2 position;
	public PointOpenStatus status;
	public int depth;

	public PointData(Vector2 position, PointOpenStatus status, int depth) {
		this.position = position;
		this.status = status;
		this.depth = depth;
	}
}