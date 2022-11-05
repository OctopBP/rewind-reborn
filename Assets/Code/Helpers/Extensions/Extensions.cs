using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using UnityEngine;
using static LanguageExt.Prelude;

namespace Rewind.Extensions {
	public static partial class Extensions {
		public static string echo<T>(this T self, string name) => $"{name} = {self}";
		public static int abs(this int self) => Mathf.Abs(self);
		public static float abs(this float self) => Mathf.Abs(self);
		public static float clamp01(this float self) => Mathf.Clamp01(self);
		public static float clamp(this float self, float min, float max) => Mathf.Clamp(self, min, max);
		public static float remap0(this float self, float from, float to) => self * (to / from);
		public static float remap(this float self, float fromMin, float fromMax, float toMin, float toMax)
			=> toMin + (toMax - toMin) * ((self - fromMin) / (fromMax - fromMin));
		public static int sign(this float self) => self switch { < 0 => -1, > 0 => 1, _ => 0, };
		public static int roundToInt(this float self) => Mathf.RoundToInt(self);

		public static bool anyAxisIsZero(this Vector2 self) => self.x == 0 || self.y == 0;
		public static bool anyAxisIsZero(this Vector3 self) => self.x == 0 || self.y == 0 || self.z == 0;

		public static Option<int> divide(this int self, int by)
			=> by == 0 ? None : self / by;
		public static Option<float> divide(this float self, float by)
			=> by == 0 ? None : self / by;
		public static Option<Vector2> divide(this Vector2 self, Vector2 by)
			=> by.x == 0 || by.y == 0 ? None :self / by;
		public static Option<Vector2> divide(this Vector2 self, float by)
			=> by == 0 ? None :self / by;
		public static Option<Vector3> divide(this Vector3 self, float by)
			=> by == 0 ? None :self / by;
		
		public static int positiveMod(this int self, int by) => (self % by + by) % by;
		public static float positiveMod(this float self, float by) => ((self % by) + by) % by;

		public static bool firstValueOut<T>(this List<T> self, out T first) {
			first = self.FirstOrDefault();
			return self.Count > 0;
		}

		public static Color withAlpha(this Color self, float alpha) => new(self.r, self.g, self.b, alpha);

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

		/// <summary>Возвращает колличество родителей</summary>
		public static int getParentsCount(this Transform transform) {
			var count = 0;
			var currentTransform = transform;

			while (currentTransform.parent != null) {
				count++;
				currentTransform = currentTransform.parent;
			}

			return count;
		}

		public static void setActive(this Component component) => component.gameObject.SetActive(true);
		public static void setInactive(this Component component) => component.gameObject.SetActive(false);
		public static void setActive(this GameObject gameObject) => gameObject.SetActive(true);
		public static void setInactive(this GameObject gameObject) => gameObject.SetActive(false);
	}
}