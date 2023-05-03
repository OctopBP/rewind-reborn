using System;
using Rewind.SharedData;
using UnityEngine;

[Serializable, GenConstructor]
public partial class PointData {
	public Vector2 localPosition;
	public PointOpenStatus status;
	public int depth;
}