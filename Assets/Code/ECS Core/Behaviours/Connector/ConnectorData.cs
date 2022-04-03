using UnityEngine;

namespace Rewind.Data {
	[CreateAssetMenu(fileName = "PlatformA", menuName = "Data/Elements/Platform A", order = 1)]
	public class ConnectorData : ScriptableObject {
		public AnimationCurve curve;
		public float openLimit;
		public float time = 1;
	}
}