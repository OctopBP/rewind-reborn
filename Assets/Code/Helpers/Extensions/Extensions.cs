using UnityEngine;

namespace Rewind.Extensions {
	public static partial class Extensions {
		public static int abs(this int self) => Mathf.Abs(self);
		public static float abs(this float self) => Mathf.Abs(self);
	}
}