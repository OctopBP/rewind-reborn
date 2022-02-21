using UnityEngine;

namespace Rewind.Data {
	[CreateAssetMenu(fileName = "GearTypeA", menuName = "Data/Elements/Gear Type A", order = 1)]
	public class GearTypeAData : ScriptableObject {
		public float rotateLimit;
		public float rotateSpeed;
	}
}