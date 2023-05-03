using System;
using UnityEngine;

namespace Rewind.SharedData {
	[Serializable]
	public partial class PendulumData {
		[SerializeField, PublicAccessor] float swayLimit;
		[SerializeField, PublicAccessor] float openLimit;
		[SerializeField, PublicAccessor] float swayPeriod = 1;
	}
}