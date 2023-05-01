using System;
using System.Collections.Generic;
using Code.Base;
using ExhaustiveMatching;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs {
	[Serializable]
	public class GraphItemInfo {
		public enum GraphType {
			Status = 0,
			RotationX = 1,
			RotationY = 2,
			RotationZ = 3,
			LocalRotationX = 4,
			LocalRotationY = 5,
			LocalRotationZ = 6,
			PositionX = 7,
			PositionY = 8,
			PositionZ = 9,
			LocalPositionX = 10,
			LocalPositionY = 11,
			LocalPositionZ = 12
		}

		public Transform target;
		public GraphType type;
		public Color color;

		public List<DataValue> data { get; } = new();

		public record DataValue(float value, float time) {
			public float value { get; } = value;
			public float time { get; } = time;
		}

		public void writeValue() {
			if (target != null) {
				data.Add(new(getValue, Time.time));
			}
		}

		float getValue => type switch {
			GraphType.RotationX => target.eulerAngles.x,
			GraphType.RotationY => target.eulerAngles.y,
			GraphType.RotationZ => target.eulerAngles.z,
			GraphType.PositionX => target.position.x,
			GraphType.PositionY => target.position.y,
			GraphType.PositionZ => target.position.z,
			GraphType.LocalRotationX => target.localEulerAngles.x,
			GraphType.LocalRotationY => target.localEulerAngles.y,
			GraphType.LocalRotationZ => target.localEulerAngles.z,
			GraphType.LocalPositionX => target.localPosition.x,
			GraphType.LocalPositionY => target.localPosition.y,
			GraphType.LocalPositionZ => target.localPosition.z,
			GraphType.Status => target.TryGetComponent(out IStatusValue sv) ? sv.statusValue.IfNone(() => -1) : -1,
			_ => throw ExhaustiveMatch.Failed(type)
		};
	}
	
	[Serializable]
	public class GraphInfo {
		[SerializeField, TableList, VerticalGroup("List"), TableColumnWidth(120)] public List<GraphItemInfo> items;
		[SerializeField, VerticalGroup("Settings")] public Vector2 scale = Vector2.one;
		[SerializeField, VerticalGroup("Settings")] public Vector2 gridSize = new(10, 10);
		[SerializeField, VerticalGroup("Settings")] public float yOffset;
		[SerializeField, HorizontalGroup("Settings/Bool")] public bool showTimelines = true;
		[SerializeField, HorizontalGroup("Settings/Bool")] public bool showGrid = true;

		public void writeValue() {
			foreach (var item in items)
				item.writeValue();
		}
	}
}