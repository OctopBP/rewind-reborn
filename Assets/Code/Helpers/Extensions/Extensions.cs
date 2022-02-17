using UnityEngine;

namespace Rewind.Extensions {
	public static partial class Extensions {
		public static int abs(this int self) => Mathf.Abs(self);
		public static float abs(this float self) => Mathf.Abs(self);
		public static float clamp01(this float self) => Mathf.Clamp01(self);
		public static float clamp(this float self, float min, float max) => Mathf.Clamp(self, min, max);
	}
}