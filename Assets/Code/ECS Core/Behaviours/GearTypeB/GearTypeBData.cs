using UnityEngine;

namespace Rewind.Data {
	[CreateAssetMenu(fileName = "GearTypeB", menuName = "Data/Elements/Gear Type B", order = 1)]
	public class GearTypeBData : ScriptableObject {
		public float multiplier;
		public float offset;
	}
}