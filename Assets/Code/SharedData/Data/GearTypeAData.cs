using System;
using UnityEngine;

namespace Rewind.SharedData
{
	[Serializable]
	public partial class GearTypeAData
    {
		[SerializeField, PublicAccessor] private float rotateLimit;
		[SerializeField, PublicAccessor] private float rotateSpeed;
	}
}