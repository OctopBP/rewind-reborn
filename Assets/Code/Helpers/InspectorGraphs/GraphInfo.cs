using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs {
	[Serializable]
	public class GraphInfo {
		public enum GraphType {
			RotationX = 0,
			RotationY = 1,
			RotationZ = 2,
			PositionX = 3,
			PositionY = 4,
			PositionZ = 5
		}

		[SerializeField] public Transform target;
		[SerializeField] public GraphType type;

		[SerializeField, Range(0.01f, 10)] public float xScale = 1;
		[SerializeField, Range(0.01f, 10)] public float yScale = 1;

		public List<float> data { get; } = new();

		public void writeValue() {
			if (target != null) {
				data.Add(getValue);
			}
		}

		float getValue => type switch {
			GraphType.RotationX => target.eulerAngles.x,
			GraphType.RotationY => target.eulerAngles.y,
			GraphType.RotationZ => target.eulerAngles.z,
			GraphType.PositionX => target.position.x,
			GraphType.PositionY => target.position.y,
			GraphType.PositionZ => target.position.z
		};
	}
}