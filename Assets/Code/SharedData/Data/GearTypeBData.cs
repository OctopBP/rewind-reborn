using System;
using UnityEngine;

namespace Rewind.SharedData {
	[Serializable]
	public partial class GearTypeBData {
		[SerializeField, PublicAccessor] float multiplier;
		[SerializeField, PublicAccessor] float offset;
	}
}