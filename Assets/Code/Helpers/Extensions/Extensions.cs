using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using UnityEngine;
using static LanguageExt.Prelude;

namespace Rewind.Extensions
{
	public static partial class Extensions
	{
		public static string Echo<T>(this T self, string name) => $"{name} = {self}";
		public static int Abs(this int self) => Mathf.Abs(self);
		public static float Abs(this float self) => Mathf.Abs(self);
		public static float Clamp01(this float self) => Mathf.Clamp01(self);
		public static float Clamp(this float self, float min, float max) => Mathf.Clamp(self, min, max);
		public static float Remap0(this float self, float from, float to) => self * (to / from);
		public static float Remap(this float self, float fromMin, float fromMax, float toMin, float toMax)
			=> toMin + (toMax - toMin) * ((self - fromMin) / (fromMax - fromMin));
		public static int Sign(this float self) => self switch { < 0 => -1, > 0 => 1, _ => 0, };
		public static int RoundToInt(this float self) => Mathf.RoundToInt(self);

		public static Option<int> Divide(this int self, int by)
			=> by == 0 ? None : self / by;
		public static Option<float> Divide(this float self, float by)
			=> by == 0 ? None : self / by;
		
		public static int PositiveMod(this int self, int by) => (self % by + by) % by;
		public static float PositiveMod(this float self, float by) => ((self % by) + by) % by;

		public static bool FirstValueOut<T>(this List<T> self, out T first) {
			first = self.FirstOrDefault();
			return self.Count > 0;
		}

		public static Color WithAlpha(this Color self, float alpha) => new(self.r, self.g, self.b, alpha);

		public static float To0Or1(this bool self) => self ? 1 : 0;
		
		/// <returns> Parents count. </returns>
		public static int GetParentsCount(this Transform transform)
        {
			var count = 0;
			var currentTransform = transform;

			while (currentTransform.parent != null)
            {
				count++;
				currentTransform = currentTransform.parent;
			}

			return count;
		}

		public static void SetActive(this Component component) => component.gameObject.SetActive(true);
		public static void SetInactive(this Component component) => component.gameObject.SetActive(false);
		public static void SetActive(this GameObject gameObject) => gameObject.SetActive(true);
		public static void SetInactive(this GameObject gameObject) => gameObject.SetActive(false);

		public static Option<T> GetComponent<T>(this Component component) =>
			component.TryGetComponent(out T t) ? t : None;
		
		public static TTo Upcast<TFrom, TTo>(this TFrom any) where TFrom : TTo => any;
		
		/// <summary> Safely upcasts type. Uses example parameter to infer To. </summary>
		public static TTo Upcast<TFrom, TTo>(this TFrom any, in TTo example) where TFrom : TTo => any;
		
		public static IEnumerable<TTo> Upcast<TFrom, TTo>(this IEnumerable<TFrom> any) where TFrom : TTo
			=> any.Select(_ => _.Upcast<TFrom, TTo>());

		/// <summary> Safely casts from Parent to Child. Uses example parameter to infer Child. </summary>
		/// <returns> When casted to wrong 'Child', returns None, else returns Some(Child). </returns>
		public static Option<TChild> Downcast<TParent, TChild>(
			this TParent value, in TChild example
		) where TChild : TParent => value is TChild casted ? casted : None;
		
		/// <summary> Just mark that this is used for side effect only </summary>
		public static void ForSideEffect<T>(this T self) { }
	}
}