using System;
using UnityEngine;

namespace Rewind.SharedData
{
	[Serializable]
	public partial class PendulumData
    {
		[SerializeField, PublicAccessor] private float swayLimit;
		[SerializeField, PublicAccessor] private float openLimit;
		[SerializeField, PublicAccessor] private float swayPeriod = 1;
	}
}