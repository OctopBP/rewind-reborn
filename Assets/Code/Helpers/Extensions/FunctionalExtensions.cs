using System;
using LanguageExt;

namespace Rewind.Extensions {
	public static class FunctionalExtensions {
		public static Option<T> some<T>(this T self) => Option<T>.Some(self);

		public static bool valueOut<T>(this Option<T> self, out T value) {
			value = self.Match(v => v, () => default);
			return self.IsSome;
		}

		public static T with<T>(this T self, Action<T> set) {
			set.Invoke(self);
			return self;
		}

		public static T with<T>(this T self, Action<T> apply, Func<bool> when) {
			if (when()) apply?.Invoke(self);
			return self;
		}

		public static T with<T>(this T self, Action<T> apply, Func<T, bool> when) {
			if (when(self)) apply?.Invoke(self);
			return self;
		}

		public static T with<T>(this T self, Action<T> apply, bool when) {
			if (when) apply?.Invoke(self);
			return self;
		}

		public static T @do<T>(this T @self, Action<T> @this, bool @when) {
			if (when) @this?.Invoke(self);
			return self;
		}
	}
}