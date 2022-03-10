using LanguageExt;
using UnityEngine;

namespace Rewind.Extensions {
	public static partial class Extensions {
		public static string echo<T>(this T self) => $"{nameof(self)} = {self}";
		public static int abs(this int self) => Mathf.Abs(self);
		public static float abs(this float self) => Mathf.Abs(self);
		public static float clamp01(this float self) => Mathf.Clamp01(self);
		public static float clamp(this float self, float min, float max) => Mathf.Clamp(self, min, max);
		public static float remap0(this float self, float from, float to) => self * (to / from);
		public static float remap(this float self, float fromMin, float fromMax, float toMin, float toMax)
			=> toMin + (toMax - toMin) * ((self - fromMin) / (fromMax - fromMin));

		public static int roundToInt(this float self) => Mathf.RoundToInt(self);

		public static bool anyAxisIsZero(this Vector2 self) => self.x == 0 || self.y == 0;
		public static bool anyAxisIsZero(this Vector3 self) => self.x == 0 || self.y == 0 || self.z == 0;

		public static Option<int> divide(this int self, int by)
			=> by == 0 ? Option<int>.None : self / by;
		public static Option<float> divide(this float self, float by)
			=> by == 0 ? Option<float>.None : self / by;
		public static Option<Vector2> divide(this Vector2 self, Vector2 by)
			=> by.x == 0 || by.y == 0 ? Option<Vector2>.None :self / by;
		public static Option<Vector2> divide(this Vector2 self, float by)
			=> by == 0 ? Option<Vector2>.None :self / by;
		public static Option<Vector3> divide(this Vector3 self, float by)
			=> by == 0 ? Option<Vector3>.None :self / by;
		
		public static int positiveMod(this int self, int by) => ((self % by) + by) % by;
		public static float positiveMod(this float self, float by) => ((self % by) + by) % by;
	}
}