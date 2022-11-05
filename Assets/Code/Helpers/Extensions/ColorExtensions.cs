using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rewind.Extensions {
	public static class ColorExtensions {
		public static Color randomColorForGuid(Guid guid) {
			var hash = guid.GetHashCode();
			Random.InitState(hash);
			return Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
		}
	}
}



