using UnityEngine;

namespace Rewind.Data {
	[CreateAssetMenu(fileName = "Pendulum", menuName = "Data/Elements/Pendulum", order = 1)]
	public class PendulumData : ScriptableObject {
		public float swayLimit;
		public float openLimit;
		public float swayPeriod = 1;
	}
}