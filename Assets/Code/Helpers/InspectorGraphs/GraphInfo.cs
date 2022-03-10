using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs {
	[Serializable]
	public class GraphItemInfo {
		public enum GraphType {
			RotationX = 0,
			RotationY = 1,
			RotationZ = 2,
			PositionX = 3,
			PositionY = 4,
			PositionZ = 5
		}

		public Transform target;
		public GraphType type;
		public Color color;

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
	
	[Serializable]
	public class GraphInfo {
		[SerializeField, TableList, VerticalGroup("List"), TableColumnWidth(120)] public List<GraphItemInfo> items;
		[SerializeField, VerticalGroup("Settings")] public Vector2 scale = Vector2.one;
		[SerializeField, VerticalGroup("Settings")] public Vector2 gridSize = new(10, 10);
		[SerializeField, VerticalGroup("Settings")] public float yOffset;

		public void writeValue() {
			foreach (var item in items)
				item.writeValue();
		}
	}
}