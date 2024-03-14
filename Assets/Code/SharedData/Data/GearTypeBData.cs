using System;
using UnityEngine;

namespace Rewind.SharedData
{
	[Serializable]
	public partial class GearTypeBData
    {
		[SerializeField, PublicAccessor] private float multiplier;
		[SerializeField, PublicAccessor] private float offset;
	}
}