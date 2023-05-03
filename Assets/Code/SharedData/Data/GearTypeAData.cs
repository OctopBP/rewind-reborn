using System;
using UnityEngine;

namespace Rewind.SharedData {
	[Serializable]
	public partial class GearTypeAData {
		[SerializeField, PublicAccessor] float rotateLimit;
		[SerializeField, PublicAccessor] float rotateSpeed;
	}
}