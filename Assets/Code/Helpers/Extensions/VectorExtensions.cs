using LanguageExt;
using UnityEngine;
using static LanguageExt.Prelude;

namespace Rewind.Extensions {
	public static class VectorExtensions {
		public static Vector3 withX(this Vector3 self, float x) => new Vector3(x, self.y, self.z);
		public static Vector3 withY(this Vector3 self, float y) => new Vector3(self.x, y, self.z);
		public static Vector3 withZ(this Vector3 self, float z) => new Vector3(self.x, self.y, z);
		
		public static bool anyAxisIsZero(this Vector2 self) => self.x == 0 || self.y == 0;
		public static bool anyAxisIsZero(this Vector3 self) => self.x == 0 || self.y == 0 || self.z == 0;
		
		public static Option<Vector2> divide(this Vector2 self, Vector2 by) =>
			by.x == 0 || by.y == 0 ? None :self / by;
		public static Option<Vector2> divide(this Vector2 self, float by) =>
			by == 0 ? None :self / by;
		public static Option<Vector3> divide(this Vector3 self, float by) =>
			by == 0 ? None :self / by;

		public static Vector2 pointOnCircle(
			int index, int max, float radius, float degAngleOffset = 0, Vector2 center = default
		) {
			if (max == 0) return Vector2.zero;
			var angle = 2 * ((float) index / max) * Mathf.PI - degAngleOffset * Mathf.Deg2Rad;
			var x = Mathf.Sin(angle);
			var y = Mathf.Cos(angle);
			return center + new Vector2(x, y) * radius;
		}

		public static Vector2 xy(this Vector3 self) => new(self.x, self.y);
		public static Vector3 withZ(this Vector2 self, float z) => new(self.x, self.y, z);
	}
}



