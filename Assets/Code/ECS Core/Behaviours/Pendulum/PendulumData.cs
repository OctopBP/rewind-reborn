using System;
using UnityEngine;

namespace Rewind.Data {
	[Serializable]
	public class PendulumData {
		public float swayLimit;
		public float openLimit;
		public float swayPeriod = 1;
	}
}