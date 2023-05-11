using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Rewind.Extensions {
	public static class FunctionalExtensions {
		public static Option<B> flatMap<A, B>(this Option<A> self, Func<A, Option<B>> f) =>
			self.Map(f).Flatten();

		public static Option<(A, B)> zip<A, B>(this Option<A> self, Option<B> other) =>
			self.flatMap(a => other.Map(b => (a, b)));

		public static Option<(A, B, C)> zip<A, B, C>(this Option<A> self, Option<B> other1, Option<C> other2) =>
			self.flatMap(a => other1.flatMap(b => other2.Map(c => (a, b, c))));

		public static Option<C> zip<A, B, C>(this Option<A> self, Option<B> other, Func<A, B, C> f) =>
			self.flatMap(a => other.Map(b => f(a, b)));
		
		public static Option<D> zip<A, B, C, D>(
			this Option<A> self, Option<B> other1, Option<C> other2, Func<A, B, C, D> f
		) => self.flatMap(a => other1.flatMap(b => other2.Map(c => f(a, b, c))));

		public static Option<T> some<T>(this T self) => Option<T>.Some(self);
		public static Option<T> optionFromNullable<T>(this T self) => self == null ? None : Some(self);
		
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
		
		public static T getOrThrow<T>(this Option<T> self, string message) =>
			self.IfNone(() => throw new Exception(message));
	}
}