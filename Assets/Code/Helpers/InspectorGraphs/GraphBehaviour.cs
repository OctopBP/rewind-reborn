using System.Collections.Generic;
using UnityEngine;

namespace Code.Helpers.InspectorGraphs {
	public class GraphBehaviour : MonoBehaviour {
		[SerializeField] float xScale = 1;
		[SerializeField] float yScale = 1;
		[SerializeField] int accuracy = 1;

		int tics;

		public float getXScale => xScale;
		public float getYScale => yScale;
		public List<float> data { get; } = new();

		protected virtual void setData() { }

		void Update() {
			if (tics++ % accuracy == 0)
				setData();
		}
	}
}