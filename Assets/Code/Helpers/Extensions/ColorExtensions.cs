using System;
using Code.Helpers;
using UnityEngine;

namespace Rewind.Extensions {
	public static class ColorExtensions {
		public static Color randomColorForGuid(Guid guid) {
			var hash = guid.GetHashCode();
			
			var colors = new[] {
				ColorA.red, ColorA.orange, ColorA.yellow, ColorA.green,
				ColorA.mint, ColorA.teal, ColorA.cyan, ColorA.blue,
				ColorA.indigo, ColorA.purple, ColorA.pink
			};
			
			return colors[Math.Abs(hash) % colors.Length];
		}
	}
}



