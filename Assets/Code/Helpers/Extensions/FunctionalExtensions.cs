using System;
using System.Collections.Generic;
using LanguageExt;

namespace Rewind.Extensions {
	public static class FunctionalExtensions {
		public static Option<T> some<T>(this T self) => Option<T>.Some(self);

		public static bool valueOut<T>(this Option<T> self, out T value) where T : new() {
			value = self.Match(v => v, () => new());
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

		public static Option<T> maybeFirst<T>(this List<T> self) =>
			self.Count > 0 ? Option<T>.Some(self[0]) : Option<T>.None;
	}
}