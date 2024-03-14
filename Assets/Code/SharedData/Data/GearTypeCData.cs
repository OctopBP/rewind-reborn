using System;
using UnityEngine;

namespace Rewind.SharedData
{
	[Serializable]
	public partial class GearTypeCData
    {
		[SerializeField, PublicAccessor] private float rotateSpeed;
	}
}