using System;
using Rewind.ECSCore.Enums;
using UnityEngine;

[Serializable]
public class PointData {
	public Vector2 localPosition;
	public PointOpenStatus status;
	public int depth;

	public PointData(Vector2 localPosition, PointOpenStatus status, int depth) {
		this.localPosition = localPosition;
		this.status = status;
		this.depth = depth;
	}
}