using LanguageExt;
using static LanguageExt.Prelude;

namespace Rewind.Extensions {
	public static class BoolExtensions {
		/// <summary> If <see cref="condition"/> is true, then `Some(<see cref="value"/>)`, otherwise None. </summary>
		public static Option<T> opt<T>(this bool condition, T value) => 
			condition ? value : None;
	}
}