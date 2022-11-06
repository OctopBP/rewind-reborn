using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Rewind.Extensions {
	public static class FunctionalExtensions {
		public static Option<T> some<T>(this T self) => Option<T>.Some(self);
		public static Option<T> toOption<T>(this T self) => self == null ? None : Some(self);

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

		public static Option<T> first<T>(this IEnumerable<T> self) {
			var first = self.FirstOrDefault();
			return first != null ? Some(first) : None;
		}
		
		public static Option<T> first<T>(this IEnumerable<T> self, Func<T, bool> predicate) {
			var first = self.FirstOrDefault(predicate);
			return first != null ? Some(first) : None;
		}

		public static Option<T> getOrFirstSome<T>(this Option<T> self, params Option<T>[] others) {
			if (self.IsSome) return self;

			foreach (var other in others) {
				if (other.IsSome) return other;
			}
			
			return None;
		}
	}
}