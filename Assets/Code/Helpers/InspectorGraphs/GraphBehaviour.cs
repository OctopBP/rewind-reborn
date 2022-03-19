using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs {
	public class GraphBehaviour : MonoBehaviour {
		[SerializeField] int accuracy = 1;
		[SerializeField, TableList] public List<GraphInfo> infos = new();

		Init init;

		void Start() {
			init = new(infos, accuracy);
		}

		void LateUpdate() {
			init.update();
		}

		class Init {
			readonly List<GraphInfo> infos;
			readonly int accuracy;
				
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