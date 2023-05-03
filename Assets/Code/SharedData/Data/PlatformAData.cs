using System;
using UnityEngine;

namespace Rewind.SharedData {
	[Serializable]
	public partial class PlatformAData {
		[SerializeField, PublicAccessor] AnimationCurve curve;
		[SerializeField, PublicAccessor] float time = 1;
	}
}