using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs {
	public class GraphBehaviour : MonoBehaviour {
		[SerializeField] int accuracy = 1;
		[SerializeField, TableList] public List<GraphInfo> infos = new();

		public static Init init;

		void Start() {
			init = new(infos, accuracy);
		}

		// void LateUpdate() {
		void FixedUpdate() {
			init.update();
		}

		public class Init {
			readonly List<GraphInfo> infos;
			readonly int accuracy;

			public record TimeLine(float time, TimeLine.Type type) {
				public enum Type { StartRecord, Record, Rewind, Replay }

				public float time { get; } = time;
				public Type type { get; } = type;
			}

			public readonly List<TimeLine> timeLines = new();
				
			int tics;

			public Init(List<GraphInfo> infos, int accuracy) {
				this.infos = infos;
				this.accuracy = accuracy;
			}

			public 	void update() {
				if (tics++ % accuracy != 0) return;

				foreach (var info in infos) {
					info.writeValue();
				}
			}
		}
	}
}