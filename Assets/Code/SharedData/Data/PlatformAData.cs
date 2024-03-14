using System;
using UnityEngine;

namespace Rewind.SharedData
{
	[Serializable]
	public partial class PlatformAData
    {
		[SerializeField, PublicAccessor] private AnimationCurve curve;
		[SerializeField, PublicAccessor] private float time = 1;
	}
}