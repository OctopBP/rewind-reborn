using System;

namespace Rewind.Extensions {
	public static class FunctionalExtensions {
		public static T with<T>(this T self, Action<T> set) {
			set.Invoke(self);
			return self;
		}

		public static T with<T>(this T self, Action<T> apply, Func<bool> when) {
			if (when())
				apply?.Invoke(self);

			return self;
		}

		public static T with<T>(this T self, Action<T> apply, Func<T, bool> when) {
			if (when(self))
				apply?.Invoke(self);

			return self;
		}

		public static T with<T>(this T self, Action<T> apply, bool when) {
			if (when)
				apply?.Invoke(self);

			return self;
		}

		public static T @do<T>(this T @self, Action<T> @this, bool @when) {
			if (when)
				@this?.Invoke(self);

			return self;
		}
	}
}