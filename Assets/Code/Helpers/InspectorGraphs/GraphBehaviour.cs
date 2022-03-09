using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs {
	public class GraphBehaviour : MonoBehaviour {
		[SerializeField] int accuracy = 1;
		[SerializeField, TableList] public List<GraphInfo> infos = new();

		int tics;

		void setData() {
			foreach (var info in infos)
				info.writeValue();
		}

		void Update() {
			if (tics++ % accuracy == 0)
				setData();
		}
	}
}