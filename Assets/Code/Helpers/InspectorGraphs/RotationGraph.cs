using UnityEngine;

namespace Code.Helpers.InspectorGraphs {
	public class RotationGraph : GraphBehaviour {
		[SerializeField] Transform target;

		protected override void setData() => data.Add(target.eulerAngles.z);
	}
}