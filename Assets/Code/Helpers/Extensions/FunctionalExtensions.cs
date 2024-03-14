using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Rewind.Extensions
{
	public static class FunctionalExtensions
    {
		public static Option<TB> FlatMap<TA, TB>(this Option<TA> self, Func<TA, Option<TB>> f) =>
			self.Map(f).Flatten();

		public static Option<(TA, TB)> Zip<TA, TB>(this Option<TA> self, Option<TB> other) =>
			self.FlatMap(a => other.Map(b => (a, b)));

		public static Option<(TA, TB, TC)> Zip<TA, TB, TC>(this Option<TA> self, Option<TB> other1, Option<TC> other2) =>
			self.FlatMap(a => other1.FlatMap(b => other2.Map(c => (a, b, c))));

		public static Option<TC> Zip<TA, TB, TC>(this Option<TA> self, Option<TB> other, Func<TA, TB, TC> f) =>
			self.FlatMap(a => other.Map(b => f(a, b)));
		
		public static Option<TD> Zip<TA, TB, TC, TD>(
			this Option<TA> self, Option<TB> other1, Option<TC> other2, Func<TA, TB, TC, TD> f
		) => self.FlatMap(a => other1.FlatMap(b => other2.Map(c => f(a, b, c))));

		public static Option<T> OptionFromNullable<T>(this T self) => self == null ? None : Some(self);
		
		public static T With<T>(this T self, Action<T> set)
        {
			set.Invoke(self);
			return self;
		}

		public static T With<T>(this T self, Action<T> apply, Func<bool> when)
        {
			if (when()) apply?.Invoke(self);
			return self;
		}

		public static T With<T>(this T self, Action<T> apply, Func<T, bool> when)
        {
			if (when(self)) apply?.Invoke(self);
			return self;
		}

		public static T With<T>(this T self, Action<T> apply, bool when)
        {
			if (when) apply?.Invoke(self);
			return self;
		}

		public static Option<T> First<T>(this IEnumerable<T> self)
        {
			var first = self.FirstOrDefault();
			return first != null ? Some(first) : None;
		}
		
		public static Option<T> First<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
			var first = self.FirstOrDefault(predicate);
			return first != null ? Some(first) : None;
		}

		public static Option<T> GetOrFirstSome<T>(this Option<T> self, params Option<T>[] others)
        {
			if (self.IsSome) return self;

			foreach (var other in others)
            {
				if (other.IsSome) return other;
			}
			
			return None;
		}
		
		public static T GetOrThrow<T>(this Option<T> self, string message) =>
			self.IfNone(() => throw new Exception(message));
	}
}